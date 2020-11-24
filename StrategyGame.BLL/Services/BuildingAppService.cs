using AutoMapper;
using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.Interfaces;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Entities.Buildings;
using StrategyGame.MODEL.Exceptions;
using StrategyGame.MODEL.Interfaces;
using StrategyGame.MODEL.DataTransferModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Services
{
    public class BuildingAppService : IBuildingAppService
    {
        private readonly IBuildingRepository buildingRepository;
        private readonly ICountyRepository countyRepository;
        private readonly IKingdomRepository kingdomRepository;
        private readonly IMapper mapper;

        public BuildingAppService(  IBuildingRepository buildingRepository,
                                    ICountyRepository countyRepository,
                                    IKingdomRepository kingdomRepository,
                                    IMapper mapper)
        {
            this.buildingRepository = buildingRepository;
            this.countyRepository = countyRepository;
            this.kingdomRepository = kingdomRepository;
            this.mapper = mapper;
        }

        public async Task<BuildingDetailDto> GetBuildingDetailAsync(Guid buildingId)
        {
            var building = await buildingRepository.GetBuildingAsync(buildingId);

            return mapper.Map<BuildingDetailDto>(building);
        }

        public async Task<IEnumerable<BuildingDto>> GetBuildingsAsync(Guid countyId)
        {
            var buildings = await buildingRepository.GetBuildingsAsync(countyId);

            var result = new List<BuildingDto>();

            foreach(var building in buildings)
            {
                result.Add(mapper.Map<BuildingDto>(building));
            }

            return result;
        }

        public async Task<BuildingNextLevelDto> GetNextLevelDetailAsync(Guid buildingId)
        {
            var building = await buildingRepository.GetBuildingAsync(buildingId);

            if(building.BuildingSpecifics.MaxLevel <= building.Level)
            {
                throw new AppException("The building is already on max LVL");
            }

            return mapper.Map<BuildingNextLevelDto>(building.BuildingSpecifics.BuildingLevels.First(x => x.Level == (building.Level + 1)));
        }

        public async Task<BuildingDetailDto> DevelopBuildingAsync(Guid buildingId)
        {
            var building = await buildingRepository.GetBuildingAsync(buildingId);

            if(building.Level >= building.BuildingSpecifics.MaxLevel)
            {
                throw new AppException("The building is already on max LVL");
            }

            var nextLvl = building.BuildingSpecifics.BuildingLevels.First(x => x.Level == (building.Level + 1));
            var county = await countyRepository.GetCountyAsync(building.CountyId);
            var kingdom = await kingdomRepository.GetKingdomAsync(county.KingdomId);

            if(CheckResources(nextLvl, county, kingdom))
            {
                await SpendResources(nextLvl, county, kingdom);
                return mapper.Map<BuildingDetailDto>(await buildingRepository.DevelopBuildingAsync(buildingId));
            }

            throw new AppException("You don't have enough resources");
        }

        private bool CheckResources(BuildingLevel nxtLvl, County county, Kingdom kingdom)
        {
            if(nxtLvl.GoldCost <= kingdom.Gold &&
                nxtLvl.MarbleCost <= county.Marble &&
                nxtLvl.WoodCost <= county.Wood &&
                nxtLvl.WineCost <= county.Wine &&
                nxtLvl.SulfurCost <= county.Sulfur)
            {
                return true;
            }

            return false;
        } 

        private async Task SpendResources(BuildingLevel nxtLvl, County county, Kingdom kingdom)
        {
            await kingdomRepository.SpendGoldAsync(kingdom.Id, nxtLvl.GoldCost);
            await countyRepository.SpendResourcesAsync(county.Id, new ResourcesDto
                                                                    {
                                                                        Wood = nxtLvl.WoodCost,
                                                                        Marble = nxtLvl.MarbleCost,
                                                                        Wine = nxtLvl.WineCost,
                                                                        Sulfur = nxtLvl.SulfurCost
                                                                    });
        }
    }
}
