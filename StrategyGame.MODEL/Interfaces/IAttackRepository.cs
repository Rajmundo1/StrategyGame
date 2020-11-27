using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Entities.Units;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.MODEL.Interfaces
{
    public interface IAttackRepository
    {
        Task<IEnumerable<Attack>> GetAttacks(Guid countyId);
        Task Attack(Guid attackerCountyId, Guid defenderCountyId, IEnumerable<Unit> units);
        Task<IEnumerable<Attack>> GetAllAttacks();
        Task RemoveAttack(Guid attackId);
    }
}
