using StrategyGame.MODEL.Entities.Buildings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.MODEL.Interfaces
{
    public interface IBuildingRepository
    {
        Task<IEnumerable<Building>> GetBuildingsAsync(Guid countyId);
        Task<Building> GetBuildingAsync(Guid buildingId);
        Task<Building> DevelopBuildingAsync(Guid buildingId);
    }
}
