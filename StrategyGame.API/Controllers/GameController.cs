using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StrategyGame.API.Common;
using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyGame.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class GameController: StrategyGameControllerBase
    {
        private readonly IGameAppService gameAppService;

        public GameController(IGameAppService gameAppService)
        {
            this.gameAppService = gameAppService;
        }


        [HttpPost("newRound")]
        public async Task NewRound()
        {
            await gameAppService.NewRound();
        }

        [HttpGet("mainPage/{kingdomId}")]
        public async Task<MainPageDto> GetMainPage([FromRoute]Guid kingdomId)
        {
            return await gameAppService.GetMainPage(kingdomId);
        }

        [HttpGet("countyPage/{countyId}")]
        public async Task<MainPageDto> GetCountyPage([FromRoute]Guid countyId)
        {
            return await gameAppService.GetCountyPage(countyId);
        }

        [HttpPut("setWineConsumption/{countyId}")]
        public async Task SetWineConsumption([FromRoute]Guid countyId, [FromQuery] int amount)
        {
            await gameAppService.SetWineConsumption(countyId, amount);
        }
    }
}
