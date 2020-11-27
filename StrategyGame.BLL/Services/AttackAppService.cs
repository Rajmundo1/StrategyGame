using AutoMapper;
using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.HelperClasses;
using StrategyGame.BLL.Interfaces;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Entities.Units;
using StrategyGame.MODEL.Exceptions;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Services
{
    public class AttackAppService : IAttackAppService
    {
        private readonly IAttackRepository attackRepository;
        private readonly ICountyRepository countyRepository;
        private readonly IUnitRepository unitRepository;
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;

        public AttackAppService(IAttackRepository attackRepository,
                                ICountyRepository countyRepository,
                                IUnitRepository unitRepository,
                                IMapper mapper,
                                IIdentityService identityService)
        {
            this.attackRepository = attackRepository;
            this.countyRepository = countyRepository;
            this.unitRepository = unitRepository;
            this.mapper = mapper;
            this.identityService = identityService;
        }

        public async Task Attack(Guid attackerCountyId, Guid defenderCountyId, IEnumerable<AttackUnitDto> units)
        {
            var currentUser = await identityService.GetCurrentUser();
            if (!(await countyRepository.IsOwner(attackerCountyId, currentUser.Id)))
            {
                throw new AppException("You aren't the owner of that county");
            }

            //check if county has enough units

            var availableUnits = new List<Unit> (await unitRepository.GetUnitsAsync(attackerCountyId));
            var unitDic = new List<UnitSpecificAndLevel>();
            var neededUnits = new List<Unit>();
            var unitsToSend = new List<Unit>();

            foreach(var unit in units)
            {
                for(int i = 0; i<unit.Count; i++)
                {
                    neededUnits.Add(new Unit
                    {
                        Level = unit.Level,
                        UnitSpecificsId = unit.UnitSpecificsId,
                    });
                }
            }


            for(int j = availableUnits.Count - 1; j >= 0 ; j--)
            {
                foreach(var unit in units)
                {
                    if (unit.UnitSpecificsId.Equals(availableUnits[j].UnitSpecificsId) &&
                    unit.Level == availableUnits[j].Level)
                    {
                        if(unit.Count - 1 < 0)
                        {
                            break;
                        }
                        else
                        {
                            unitsToSend.Add(new Unit
                            {
                                Id = availableUnits[j].Id,
                                UnitSpecificsId = availableUnits[j].UnitSpecificsId,
                                UnitGroupId = availableUnits[j].UnitGroupId,
                                Level = availableUnits[j].Level,
                                UnitSpecifics = availableUnits[j].UnitSpecifics
                            });
                            unit.Count--;
                            break;
                        }
                    }
                }
                availableUnits.RemoveAt(j);
            }

            foreach(var unit in units)
            {
                if (unit.Count > 0)
                {
                    throw new AppException("You don't have enough units for this attack");
                }
            }


            await attackRepository.Attack(attackerCountyId, defenderCountyId, unitsToSend);
        }

        public async Task<IEnumerable<AttackDto>> GetAttacks(Guid countyId)
        {
            var currentUser = await identityService.GetCurrentUser();
            if (!(await countyRepository.IsOwner(countyId, currentUser.Id)))
            {
                throw new AppException("You aren't the owner of that county");
            }

            var attacks = await attackRepository.GetAttacks(countyId);
            var result = new List<AttackDto>();
            var unitDic = new List<UnitSpecificAndLevel>();

            foreach (var attack in attacks)
            {
                var attackDto = mapper.Map<AttackDto>(attack);

                var unitDtos = new List<UnitDto>();

                foreach (var unit in attack.AttackerUnits.Units)
                {
                    var dummy = new UnitSpecificAndLevel();
                    dummy.Level = unit.Level;
                    dummy.UnitSpecificsId = unit.UnitSpecificsId;

                    var special = unitDic.SingleOrDefault(x => x.Level == dummy.Level && x.UnitSpecificsId.Equals(dummy.UnitSpecificsId));
                    if (special != null)
                    {
                        special.Count += 1;
                    }
                    else
                    {
                        dummy.Count = 1;
                        unitDic.Add(dummy);
                    }
                }

                foreach (var element in unitDic)
                {
                    var unitSpecifics = await unitRepository.GetUnitSpecificsAsync(element.UnitSpecificsId);

                    unitDtos.Add(new UnitDto
                    {
                        UnitSpecificsId = element.UnitSpecificsId,
                        Count = element.Count,
                        ImageUrl = unitSpecifics.ImageUrl,
                        MaxLevel = unitSpecifics.MaxLevel,
                        Name = unitSpecifics.Name,
                        Level = element.Level
                    });
                }

                attackDto.Units = unitDtos;

                result.Add(attackDto);
            }


            return result;
        }
    }
}
