using StrategyGame.BLL.Dtos;
using StrategyGame.MODEL.Entities.Buildings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Interfaces
{
    public interface IBuildingAppService
    {
        Task<IEnumerable<BuildingDto>> GetBuildingsAsync(Guid countyId);
        Task<BuildingDetailDto> GetBuildingDetailAsync(Guid buildingId);
        Task<BuildingNextLevelDto> GetNextLevelDetailAsync(Guid buildingId);
        Task<BuildingDetailDto> DevelopBuildingAsync(Guid buildingId);

    }
}
