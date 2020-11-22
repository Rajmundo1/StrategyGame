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
    public class TechnologyController : ControllerBase
    {
        private readonly ITechnologyAppService technologyAppService;

        public TechnologyController(ITechnologyAppService technologyAppService)
        {
            this.technologyAppService = technologyAppService;
        }

        [HttpGet("technologies/{kingdomId}")]
        public async Task<IEnumerable<TechnologyDto>> GetTechnologies([FromRoute] Guid kingdomId)
        {
            return await technologyAppService.GetTechnologies(kingdomId);
        }

        [HttpGet("technologies/{technologyId}")]
        public async Task<TechnologyDetailDto> GetTechnologyDetail([FromRoute] Guid technologyId)
        {
            return await technologyAppService.GetTechnologyDetail(technologyId);
        }

        [HttpPut("develop/{technologyId}")]
        public async Task DevelopTechnology([FromRoute] Guid technologyId)
        {
            await technologyAppService.DevelopTechnology(technologyId);
        }
    }
}
