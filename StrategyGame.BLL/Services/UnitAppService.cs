using AutoMapper;
using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.HelperClasses;
using StrategyGame.BLL.Interfaces;
using StrategyGame.MODEL.DataTransferModels;
using StrategyGame.MODEL.Entities.Units;
using StrategyGame.MODEL.Exceptions;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Services
{
    public class UnitAppService : IUnitAppService
    {
        private readonly IMapper mapper;
        private readonly IUnitRepository unitRepository;
        private readonly ICountyRepository countyRepository;
        private readonly IKingdomRepository kingdomRepository;
        private readonly IIdentityService identityService;

        public UnitAppService(IUnitRepository unitRepository,
                                ICountyRepository countyRepository,
                                IKingdomRepository kingdomRepository,
                                IMapper mapper,
                                IIdentityService identityService)
        {
            this.mapper = mapper;
            this.countyRepository = countyRepository;
            this.unitRepository = unitRepository;
            this.kingdomRepository = kingdomRepository;
            this.identityService = identityService;
        }

        public async Task DevelopUnitsAsync(int count, Guid countyId, Guid unitSpecificsId, int currentLvl)
        {
            var currentUser = await identityService.GetCurrentUser();
            if(!(await countyRepository.IsOwner(countyId, currentUser.Id)))
            {
                throw new AppException("You aren't the owner of that county");
            }

            var unitSpecifics = await unitRepository.GetUnitSpecificsAsync(unitSpecificsId);
            var unitLevel = unitSpecifics.UnitLevels.SingleOrDefault(x => x.Level == (currentLvl + 1));

            if(unitLevel == null)
            {
                throw new AppException("Unavailable LVL");
            }

            var unitsToDevelop = await unitRepository.GetUnitsBySpecificsAndLevelAsync(countyId, unitSpecificsId, currentLvl);
            if(unitsToDevelop.Count() < count)
            {
                throw new AppException("You don't have enough units for that upgrade");
            }

            //check if it has enough  resources + forcelimit
            if (await CheckAndSpendResources(count, countyId, unitSpecificsId, currentLvl + 1))
            {
                //+units
                await unitRepository.DevelopUnitsAsync(count, countyId, unitSpecificsId, currentLvl);
            }
        }

        public async Task<UnitNextLevelDto> GetNextLevelDetailAsync(Guid unitSpecificsId, int currentLvl)
        {
            var unitSpecifics = await unitRepository.GetUnitSpecificsAsync(unitSpecificsId);
            var unitLevel = unitSpecifics.UnitLevels.SingleOrDefault(x => x.Level == (currentLvl + 1));

            if (unitLevel == null)
            {
                throw new AppException("Unavailable LVL");
            }

            var result = mapper.Map<UnitNextLevelDto>(unitLevel);
            result.Name = unitSpecifics.Name;
            result.ImageUrl = unitSpecifics.ImageUrl;

            return result;
        }

        public async Task<UnitDetailsDto> GetUnitDetailsAsync(Guid unitSpecificsId, int currentLvl)
        {
            var unitSpecifics = await unitRepository.GetUnitSpecificsAsync(unitSpecificsId);
            var unitLevel = unitSpecifics.UnitLevels.SingleOrDefault(x => x.Level == currentLvl);

            if (unitLevel == null)
            {
                throw new AppException("Unavailable LVL");
            }

            var result = mapper.Map<UnitDetailsDto>(unitLevel);
            result.Name = unitSpecifics.Name;
            result.ImageUrl = unitSpecifics.ImageUrl;
            result.Description = unitSpecifics.Description;
            result.MaxLevel = unitSpecifics.MaxLevel;

            return result;
        }

        public async Task<IEnumerable<UnitDto>> GetUnitsAsync(Guid countyId)
        {
            var currentUser = await identityService.GetCurrentUser();
            if (!(await countyRepository.IsOwner(countyId, currentUser.Id)))
            {
                throw new AppException("You aren't the owner of that county");
            }

            var units = await unitRepository.GetUnitsAsync(countyId);
            var unitDic = new List<UnitSpecificIdAndLevelWithCount>();

            foreach(var unit in units)
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

            var result = new List<UnitDto>();

            foreach(var element in unitDic)
            {
                var unitSpecifics = await unitRepository.GetUnitSpecificsAsync(element.UnitSpecificsId);

                result.Add(new UnitDto
                {
                    UnitSpecificsId = element.UnitSpecificsId,
                    Count = element.Count,
                    ImageUrl = unitSpecifics.ImageUrl,
                    MaxLevel = unitSpecifics.MaxLevel,
                    Name = unitSpecifics.Name,
                    Level = element.Level
                });
            }

            return result;
        }

        public async Task<IEnumerable<UnitSpecificsDto>> GetUnitSpecifics()
        {
            var specs = await unitRepository.GetAllUnitSpecificsAsync();

            var result = new List<UnitSpecificsDto>();

            var dummy = new List<UnitSpecificsAndLevel>();

            foreach(var spec in specs)
            {
                foreach(var lvl in spec.UnitLevels)
                {
                    dummy.Add(new UnitSpecificsAndLevel { Level = lvl.Level, UnitSpecifics = spec });
                }
            }

            foreach(var element in dummy)
            {
                result.Add(mapper.Map<UnitSpecificsDto>(element));
            }

            return result;
        }

        public async Task HireUnitsAsync(int count, Guid countyId, Guid unitSpecificsId)
        {
            var currentUser = await identityService.GetCurrentUser();
            if (!(await countyRepository.IsOwner(countyId, currentUser.Id)))
            {
                throw new AppException("You aren't the owner of that county");
            }

            //check if it has enough  resources + forcelimit
            if (await CheckAndSpendResources(count, countyId, unitSpecificsId, 1))
            {
                //+units
                await unitRepository.HireUnitsAsync(count, countyId, unitSpecificsId);
            }
        }

        public async Task RemoveUnitsAsync(int count, Guid countyId, Guid unitSpecificsId, int lvl)
        {
            var currentUser = await identityService.GetCurrentUser();
            if (!(await countyRepository.IsOwner(countyId, currentUser.Id)))
            {
                throw new AppException("You aren't the owner of that county");
            }

            var units = (await unitRepository.GetUnitsAsync(countyId)).Where(x => x.UnitSpecificsId.Equals(unitSpecificsId) && x.Level == lvl).ToList();

            if(units.Count - count < 0)
            {
                throw new AppException("You don't have enough units to delete");
            }

            await unitRepository.RemoveUnitsAsync(count, countyId, unitSpecificsId, lvl);
        }

        private async Task<bool> CheckAndSpendResources(int count, Guid countyId, Guid unitSpecificsId, int lvl)
        {
            var county = await countyRepository.GetCountyAsync(countyId);
            var kingdom = await kingdomRepository.GetKingdomAsync(county.KingdomId);
            var unitSpecific = await unitRepository.GetUnitSpecificsAsync(unitSpecificsId);
            var unitLvl = unitSpecific.UnitLevels.Single(x => x.Level == lvl);

            //resource check
            if (unitLvl.GoldCost >= kingdom.Gold ||
                unitLvl.WoodCost >= county.Wood ||
                unitLvl.MarbleCost >= county.Marble ||
                unitLvl.WineCost >= county.Wine ||
                unitLvl.SulfurCost >= county.Sulfur)
            {
                throw new AppException("You don't have enough resources for the units");
            }

            //force limit check
            if ((county.MaxForceLimit - county.UsedForceLimit) <=
                    unitLvl.ForceLimit * count)
            {
                throw new AppException("You don't have enough room for the units");
            }

            await kingdomRepository.SpendGoldAsync(kingdom.Id, unitLvl.GoldCost);
            await countyRepository.SpendResourcesAsync(countyId, new ResourcesDto
            {
                Wood = unitLvl.WoodCost,
                Marble = unitLvl.MarbleCost,
                Wine = unitLvl.WineCost,
                Sulfur = unitLvl.SulfurCost
            });

            return true;
        }
    }
}
