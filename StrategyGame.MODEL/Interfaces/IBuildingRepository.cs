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
        Task<Building> GetBuilding(Guid buildingId);
        Task<Building> DevelopBuilding(Guid buildingId);
    }
}
