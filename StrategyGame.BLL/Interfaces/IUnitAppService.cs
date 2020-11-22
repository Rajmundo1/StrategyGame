using StrategyGame.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Interfaces
{
    public interface IUnitAppService
    {
        Task<IEnumerable<UnitDto>> GetUnitsAsync(Guid countyId);
        Task<UnitDetailsDto> GetUnitDetailsAsync(Guid unitSpecificsId, int currentLvl);
        Task<UnitNextLevelDto> GetNextLevelDetailAsync(Guid unitSpecificsId, int currentLvl);
        Task DevelopUnitsAsync(int count, Guid countyId, Guid unitSpecificsId, int lvl);
        Task HireUnitsAsync(int count, Guid countyId, Guid unitSpecificsId);
        Task RemoveUnitsAsync(int count, Guid countyId, Guid unitSpecificsId, int lvl);
    }
}
