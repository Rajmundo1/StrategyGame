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
        Task<Unit> GetUnitBySpecificsAndLevelAsync(Guid countyId, Guid unitSpecificsId, int currentlvl);
        Task DevelopUnitsAsync(int count, Guid countyId, Guid unitSpecificsId, int currentlvl);
        Task HireUnitsAsync(int count, Guid countyId, Guid unitSpecificsId);
        Task RemoveUnitsAsync(int count, Guid countyId, Guid unitSpecificsId, int lvl);
        Task RemoveUnitByIdAsync(Guid unitId);
        Task RemoveUnitEntityByIdAsync(Guid unitId);
        Task<UnitSpecifics> GetUnitSpecificsAsync(Guid unitSpecificsId);
        Task<IEnumerable<UnitSpecifics>> GetAllUnitSpecificsAsync();
        Task MoveToUnitGroup(Guid unitspecificsId, int lvl, int count, Guid unitGroupId);
        Task RemoveUnitGroup(Guid unitGroupId);
    }
}
