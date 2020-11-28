using Microsoft.EntityFrameworkCore;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
            return await dbContext.Kingdoms
                .Include(k => k.Counties)
                .ThenInclude(c => c.Buildings)
                .ThenInclude(b => b.BuildingSpecifics)
                .ThenInclude(bsp => bsp.BuildingLevels)
                .Include(k => k.Counties)
                .ThenInclude(c => c.Units)
                .ThenInclude(u => u.Units)
                .ThenInclude(u => u.UnitSpecifics)
                .ThenInclude(usp => usp.UnitLevels)
                .Include(k => k.Technologies)
                .ThenInclude(t => t.Specifics)
                .SingleAsync(x => x.Id.Equals(kingdomId));
        }

        public async Task<IEnumerable<Kingdom>> GetKingdomsAsync()
        {
            return await dbContext.Kingdoms
                .Include(k => k.Counties)
                .ThenInclude(c => c.Buildings)
                .ThenInclude(b => b.BuildingSpecifics)
                .ThenInclude(bsp => bsp.BuildingLevels)
                .Include(k => k.Counties)
                .ThenInclude(c => c.Units)
                .ThenInclude(u => u.Units)
                .ThenInclude(u => u.UnitSpecifics)
                .ThenInclude(usp => usp.UnitLevels)
                .Include(k => k.Technologies)
                .ThenInclude(t => t.Specifics)
                .ToListAsync();
        }

        public async Task<bool> IsOwner(Guid kingdomId, string userId)
        {
            var user = await dbContext.Users.SingleOrDefaultAsync(user => user.KingdomId.Equals(kingdomId) && user.Id.Equals(userId));

            if(user == null)
            {
                return false;
            }

            return true;
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

            await dbContext.SaveChangesAsync();
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

            await dbContext.SaveChangesAsync();
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

            await dbContext.SaveChangesAsync();
        }
    }
}
