using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StrategyGame.API.Common;
using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.Interfaces;
using StrategyGame.MODEL.Entities.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyGame.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class UnitController: StrategyGameControllerBase
    {

        private readonly IUnitAppService unitAppService;
        public UnitController(IUnitAppService unitAppService)
        {
            this.unitAppService = unitAppService;
        }

        [HttpGet("units/{countyId}")]
        public async Task<IEnumerable<UnitDto>> GetUnitsAsync([FromRoute] Guid countyId)
        {
            return await unitAppService.GetUnitsAsync(countyId);
        }

        [HttpGet("unitDetails/{unitSpecificsId}")]
        public async Task<UnitDetailsDto> GetUnitDetailsAsync([FromRoute] Guid unitSpecificsId, [FromQuery] int currentLvl)
        {
            return await unitAppService.GetUnitDetailsAsync(unitSpecificsId, currentLvl);
        }

        [HttpGet("unitNextLevelDetails/{unitSpecificsId}")]
        public async Task<UnitNextLevelDto> GetNextLevelDetailAsync([FromRoute] Guid unitSpecificsId, [FromQuery] int currentLvl)
        {
            return await unitAppService.GetNextLevelDetailAsync(unitSpecificsId, currentLvl);
        }

        [HttpPut("remove/{countyId}/{unitSpecificsId}")]
        public async Task RemoveUnitsAsync([FromRoute] Guid countyId, [FromRoute] Guid unitSpecificsId, [FromQuery] int lvl, [FromQuery] int count)
        {
            await unitAppService.RemoveUnitsAsync(count, countyId, unitSpecificsId, lvl);
        }

        [HttpPut("develop/{countyId}/{unitSpecificsId}")]
        public async Task DevelopUnitsAsync([FromRoute] Guid countyId, [FromRoute] Guid unitSpecificsId, [FromQuery] int currentLvl, [FromQuery] int count)
        {
            await unitAppService.DevelopUnitsAsync(count, countyId, unitSpecificsId, currentLvl);
        }

        [HttpPost("hire/{countyId}/{unitSpecificsId}")]
        public async Task HireUnitsAsync([FromRoute] Guid countyId, [FromRoute] Guid unitSpecificsId, [FromQuery] int count)
        {
            await unitAppService.HireUnitsAsync(count, countyId, unitSpecificsId);
        }

        [HttpGet("unispecifics")]
        public async Task<IEnumerable<UnitSpecificsDto>> GetUnitSpecifics()
        {
            return await unitAppService.GetUnitSpecifics();
        }
    }
}
