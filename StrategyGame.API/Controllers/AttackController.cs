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
    public class AttackController: StrategyGameControllerBase
    {
        private readonly IAttackAppService attackAppService;

        public AttackController(IAttackAppService attackAppService)
        {
            this.attackAppService = attackAppService;
        }

        //Task<IEnumerable<Attack>> GetAttacks(Guid countyId);
        //Task Attack(Guid attackerCountyId, Guid defenderCountyId, UnitGroup units);

        [HttpGet("attacks/{countyId}")]
        public async Task<IEnumerable<AttackDto>> GetAttacks([FromRoute]Guid countyId)
        {
            return await attackAppService.GetAttacks(countyId);
        }

        [HttpPost("attack")]
        public async Task Attack([FromQuery]Guid attackerCountyId, [FromQuery]Guid defenderCountyId, [FromBody]IEnumerable<AttackUnitDto> units)
        {
            await attackAppService.Attack(attackerCountyId, defenderCountyId, units);
        }
    }
}
