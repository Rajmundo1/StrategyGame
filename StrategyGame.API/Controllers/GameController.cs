using Hangfire;
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
    public class GameController : StrategyGameControllerBase
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

        [HttpGet("mainPage")]
        public async Task<MainPageDto> GetMainPage()
        {
            return await gameAppService.GetMainPage();
        }

        [HttpGet("countyPage/{countyId}")]
        public async Task<MainPageDto> GetCountyPage([FromRoute]Guid countyId)
        {
            var st = new System.Diagnostics.Stopwatch();
            st.Start();
            return await gameAppService.GetCountyPage(countyId);
            st.Stop();
            Console.WriteLine(st.ElapsedMilliseconds);
        }

        [HttpPut("wineCons/{countyId}")]
        public async Task WineCons([FromRoute]Guid countyId, [FromQuery] int amount)
        {
            await gameAppService.SetWineConsumption(countyId, amount);
        }

        [HttpPost("newCounty/{kingdomId}")]
        public async Task NewCounty([FromRoute] Guid kingdomId, [FromQuery] string countyName)
        {
            await gameAppService.NewCounty(kingdomId, countyName);
        }

        [HttpGet("counties/{kingdomId}")]
        public async Task<IEnumerable<CountyDto>> GetCounties(Guid kingdomId)
        {
            return await gameAppService.GetCounties(kingdomId);
        }

        [HttpGet("allCounties")]
        public async Task<IEnumerable<CountyDto>> GetAllCounties()
        {
            return await gameAppService.GetAllCounties();
        }

        [HttpGet("allCountiesFiltered")]
        public async Task<IEnumerable<CountyDto>> GetAllCounties([FromQuery] string userName)
        {
            return await gameAppService.GetAllFilteredCounties(userName);
        }
    }
}
