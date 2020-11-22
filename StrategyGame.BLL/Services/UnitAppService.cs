using AutoMapper;
using StrategyGame.BLL.Dtos;
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

        public UnitAppService(IUnitRepository unitRepository,
                                ICountyRepository countyRepository,
                                IKingdomRepository kingdomRepository,
                                IMapper mapper)
        {
            this.mapper = mapper;
            this.countyRepository = countyRepository;
            this.unitRepository = unitRepository;
            this.kingdomRepository = kingdomRepository;
        }

        public async Task DevelopUnitsAsync(int count, Guid countyId, Guid unitSpecificsId, int lvl)
        {
            var unitSpecifics = await unitRepository.GetUnitSpecificsAsync(unitSpecificsId);
            var unitLevel = unitSpecifics.UnitLevels.SingleOrDefault(x => x.Level == lvl);

            if(unitLevel == null)
            {
                throw new AppException("Unavailable LVL");
            }

            //check if it has enough  resources + forcelimit
            if (await CheckAndSpendResources(count, countyId, unitSpecificsId, lvl))
            {
                //+units
                await unitRepository.HireUnitsAsync(count, countyId, unitSpecificsId);
            }
        }

        public async Task<UnitNextLevelDto> GetNextLevelDetailAsync(Guid unitSpecificsId, int currentLvl)
        {
            var unitSpecifics = await unitRepository.GetUnitSpecificsAsync(unitSpecificsId);
            var unitLevel = unitSpecifics.UnitLevels.SingleOrDefault(x => x.Level == currentLvl++);

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
            var units = await unitRepository.GetUnitsAsync(countyId);
            var unitDictionary = new Dictionary<UnitSpecificAndLevel, int>();
            var dummy = new UnitSpecificAndLevel();

            foreach(var unit in units)
            {
                dummy.Level = unit.Level;
                dummy.UnitSpecificsId = unit.UnitSpecificsId;

                if (unitDictionary.ContainsKey(dummy))
                {
                    unitDictionary[dummy]++;
                }
                else
                {
                    unitDictionary.Add(dummy, 1);
                }
            }

            var result = new List<UnitDto>();

            foreach(var key in unitDictionary.Keys)
            {
                var unitSpecifics = await unitRepository.GetUnitSpecificsAsync(key.UnitSpecificsId);

                result.Add(new UnitDto
                {
                    UnitSpecificsId = key.UnitSpecificsId,
                    Count = unitDictionary[key],
                    ImageUrl = unitSpecifics.ImageUrl,
                    MaxLevel = unitSpecifics.MaxLevel,
                    Name = unitSpecifics.Name,
                    Level = key.Level
                });
            }

            return result;
        }

        public async Task HireUnitsAsync(int count, Guid countyId, Guid unitSpecificsId)
        {
            //check if it has enough  resources + forcelimit
            if(await CheckAndSpendResources(count, countyId, unitSpecificsId, 1))
            {
                //+units
                await unitRepository.HireUnitsAsync(count, countyId, unitSpecificsId);
            }
        }

        public async Task RemoveUnitsAsync(int count, Guid countyId, Guid unitSpecificsId, int lvl)
        {
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
            if ((county.ForceLimit - county.ForceLimitUsed) <=
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

        private class UnitSpecificAndLevel
        {
            public Guid UnitSpecificsId { get; set; }
            public int Level { get; set; }
        }
    }
}
