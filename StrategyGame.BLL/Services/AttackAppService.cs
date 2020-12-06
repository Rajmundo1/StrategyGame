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
            var unitsNeeded = new List<AttackUnitDto>(units);

            foreach(var avUnit in availableUnits)
            {
                foreach(var nUnit in unitsNeeded)
                {
                    if (nUnit.UnitSpecificsId.Equals(avUnit.UnitSpecificsId) &&
                    nUnit.Level == avUnit.Level)
                    {
                        avUnit.Count -= nUnit.Count;
                        nUnit.Count = 0;
                    }
                }
            }

            foreach(var avUnit in availableUnits)
            {
                if (avUnit.Count < 0)
                {
                    throw new AppException("You don't have enough units for this attack");
                }
            }

            var unitsToSend = new List<Unit>();
            foreach(var unit in units)
            {
                unitsToSend.Add(new Unit
                {
                    Count = unit.Count,
                    Level = unit.Level,
                    UnitGroupId = availableUnits[0].UnitGroupId,
                    UnitSpecificsId = unit.UnitSpecificsId,
                });
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

            foreach (var attack in attacks)
            {
                var attackDto = mapper.Map<AttackDto>(attack);

                var unitDtos = new List<UnitDto>();

                var unitDic = new List<UnitSpecificIdAndLevelWithCount>();

                foreach (var unit in attack.AttackerUnits.Units)
                {
                    var dummy = new UnitSpecificIdAndLevelWithCount();
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

                var resultUnits = new List<UnitDto>();

                foreach (var element in unitDic)
                {
                    var unitSpecifics = await unitRepository.GetUnitSpecificsAsync(element.UnitSpecificsId);

                    resultUnits.Add(new UnitDto
                    {
                        UnitSpecificsId = element.UnitSpecificsId,
                        Count = element.Count,
                        ImageUrl = unitSpecifics.ImageUrl,
                        MaxLevel = unitSpecifics.MaxLevel,
                        Name = unitSpecifics.Name,
                        Level = element.Level
                    });
                }

                attackDto.Units = resultUnits;

                result.Add(attackDto);

                //foreach (var unit in attack.AttackerUnits.Units)
                //{

                //    var dummy = new UnitSpecificIdAndLevelWithCount();
                //    dummy.Level = unit.Level;
                //    dummy.UnitSpecificsId = unit.UnitSpecificsId;

                //    var special = unitDic.SingleOrDefault(x => x.Level == dummy.Level && x.UnitSpecificsId.Equals(dummy.UnitSpecificsId));
                //    if (special != null)
                //    {
                //        special.Count += 1;
                //    }
                //    else
                //    {
                //        dummy.Count = 1;
                //        unitDic.Add(dummy);
                //    }
                //}

                //foreach (var element in unitDic)
                //{
                //    var unitSpecifics = await unitRepository.GetUnitSpecificsAsync(element.UnitSpecificsId);

                //    unitDtos.Add(new UnitDto
                //    {
                //        UnitSpecificsId = element.UnitSpecificsId,
                //        Count = element.Count,
                //        ImageUrl = unitSpecifics.ImageUrl,
                //        MaxLevel = unitSpecifics.MaxLevel,
                //        Name = unitSpecifics.Name,
                //        Level = element.Level
                //    });
                //}

                //attackDto.Units = unitDtos;

                //result.Add(attackDto);
            }


            return result;
        }
    }
}
