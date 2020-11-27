using StrategyGame.MODEL.DataTransferModels;
using StrategyGame.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.MODEL.Interfaces
{
    public interface ICountyRepository
    {
        Task<County> GetCountyAsync(Guid countyId);
        Task SpendResourcesAsync(Guid countyId, ResourcesDto resources);
        Task TransferResourcesAsync(Guid sourceCountyId, Guid targetCountyId, ResourcesDto resources);
        Task<bool> IsOwner(Guid countyId, string userId);
        Task SetWineConsumption(Guid countyId, int amount);
    }
}
