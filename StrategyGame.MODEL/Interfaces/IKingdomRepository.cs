using StrategyGame.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.MODEL.Interfaces
{
    public interface IKingdomRepository
    {
        Task<IEnumerable<Kingdom>> GetKingdomsAsync();
        Task<Kingdom> GetKingdomAsync(Guid kingdomId);
        Task SpendGoldAsync(Guid kingdomId, int amount);
        Task SpendResearchPointAsync(Guid kingdomId, int amount);
        Task TranferGold(Guid sourceKingdomId, Guid targetKingdomId, int amount);
        Task<bool> IsOwner(Guid kingdomId, Guid userId);
    }
}
