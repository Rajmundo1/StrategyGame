using StrategyGame.MODEL.Entities.Units;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.MODEL.Interfaces
{
    public interface IUnitRepository
    {
        Task<IEnumerable<Unit>> GetUnits(Guid countyId);
        Task DevelopUnits(int count, Guid countyId, Guid unitSpecificsId);
        Task RemoveUnits(int count, Guid countyId, Guid unitSpecificsId);
    }
}
