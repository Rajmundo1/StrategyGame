using StrategyGame.MODEL.Entities.Units;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.MODEL.Interfaces
{
    public interface IUnitRepository
    {
        Task<IEnumerable<Unit>> GetUnitsAsync(Guid countyId);
        Task DevelopUnitsAsync(int count, Guid countyId, Guid unitSpecificsId);
        Task HireUnitsAsync(int count, Guid countyId, Guid unitSpecificsId);
        Task RemoveUnitsAsync(int count, Guid countyId, Guid unitSpecificsId, int lvl);
        Task RemoveUnitByIdAsync(Guid unitId);
        Task<UnitSpecifics> GetUnitSpecificsAsync(Guid unitSpecificsId);
        Task MoveToUnitGroup(Guid unitId, Guid unitGroupId);
    }
}
