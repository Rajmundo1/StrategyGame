using Microsoft.EntityFrameworkCore;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.DAL.Repositories
{
    public class KingdomRepository : IKingdomRepository
    {
        private readonly ApplicationDbContext dbContext;
        public KingdomRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Kingdom> GetKingdomAsync(Guid kingdomId)
        {
            return await dbContext.Kingdoms.SingleAsync(x => x.Id.Equals(kingdomId));
        }

        public async Task<IEnumerable<Kingdom>> GetKingdomsAsync()
        {
            return await dbContext.Kingdoms.ToListAsync();
        }

        public async Task SpendGoldAsync(Guid kingdomId, int amount)
        {
            await dbContext.Kingdoms.ForEachAsync(kingdom =>
            {
                if (kingdom.Id.Equals(kingdomId))
                {
                    kingdom.Gold -= amount;
                }

            });
        }

        public async Task SpendResearchPointAsync(Guid kingdomId, int amount)
        {
            await dbContext.Kingdoms.ForEachAsync(kingdom =>
            {
                if (kingdom.Id.Equals(kingdomId))
                {
                    kingdom.ResearchPoint -= amount;
                }
            });
        }

        public async Task TranferGold(Guid sourceKingdomId, Guid targetKingdomId, int amount)
        {
            await dbContext.Kingdoms.ForEachAsync(x =>
            {
                if (x.Id.Equals(sourceKingdomId))
                {
                    x.Gold -= amount;
                }
                if (x.Id.Equals(targetKingdomId))
                {
                    x.Gold += amount;
                }
            });
        }
    }
}
