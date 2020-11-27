using StrategyGame.BLL.Dtos;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Entities.Units;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Interfaces
{
    public interface IAttackAppService
    {
        Task<IEnumerable<AttackDto>> GetAttacks(Guid countyId);
        Task Attack(Guid attackerCountyId, Guid defenderCountyId, IEnumerable<AttackUnitDto> units);
    }
}
