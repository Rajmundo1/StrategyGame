using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StrategyGame.BLL.Dtos;
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
    public class GameController: ControllerBase
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
    }
}
