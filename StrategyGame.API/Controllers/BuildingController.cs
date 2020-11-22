using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StrategyGame.API.Common;
using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.Interfaces;
using StrategyGame.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyGame.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class BuildingController: StrategyGameControllerBase
    {
        private readonly IBuildingAppService buildingService;

        public BuildingController(IBuildingAppService buildingService)
        {
            this.buildingService = buildingService;
        }

        [HttpGet("buildings/{countyId}")]
        public async Task<IEnumerable<BuildingDto>> GetBuildingsAsync([FromRoute] Guid countyId)
        {
            return await buildingService.GetBuildingsAsync(countyId);
        }

        [HttpGet("buildingDetail/{buildingId}")]
        public async Task<BuildingDetailDto> GetBuildingDetailAsync([FromRoute] Guid buildingId)
        {
            return await buildingService.GetBuildingDetailAsync(buildingId);
        }

        [HttpGet("buildingNextLevelDetail/{buildingId}")]
        public async Task<BuildingNextLevelDto> GetNextLevelDetailAsync([FromRoute] Guid buildingId)
        {
            return await buildingService.GetNextLevelDetailAsync(buildingId);
        }

        [HttpPost]
        public async Task DevelopBuildingAsync([FromRoute] Guid buildingId)
        {
           await buildingService.DevelopBuildingAsync(buildingId);
        }
    }
}
