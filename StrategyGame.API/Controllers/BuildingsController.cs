using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StrategyGame.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BuildingsController: ControllerBase
    {
        private readonly IBuildingAppService buildingService;

        public BuildingsController(IBuildingAppService buildingService)
        {
            this.buildingService = buildingService;
        }
        //TODO
        //[HttpGet]
        //public async Task<IEnumerable<BuildingInfoViewModel>> GetBuildings()
        //{
        //    int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //    return await buildingService.GetBuildings();
        //}

        //[HttpPost("build")]
        //public async Task<BuildingInfoViewModel> BuildBuilding([FromBody] IdDTO buildingId)
        //{
        //    int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        //    await buildingService.PurchaseBuildingByIdAsync(userId, buildingId.Id);
        //    var buildings = await GetBuildingInfos();
        //    return buildings.Single(b => b.Id == buildingId.Id);
        //}
    }
}
