using Microsoft.EntityFrameworkCore;
using StrategyGame.MODEL.DataTransferModels;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.DAL.Repositories
{
    public class CountyRepository : ICountyRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CountyRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<County> GetCountyAsync(Guid countyId)
        {
            return await dbContext.Counties
                .Include(county => county.Kingdom)
                .ThenInclude(kingdom => kingdom.Technologies)
                .ThenInclude(tech => tech.Specifics)
                .Include(county => county.Buildings)
                .ThenInclude(b => b.BuildingSpecifics)
                .ThenInclude(sp => sp.BuildingLevels)
                .Include(county => county.Units)
                .ThenInclude(u => u.Units)
                .ThenInclude(u => u.UnitSpecifics)
                .ThenInclude(usp => usp.UnitLevels)
                .SingleAsync(x => x.Id.Equals(countyId));
        }

        public async Task<bool> IsOwner(Guid countyId, string userId)
        {
            var county = await dbContext.Counties.SingleAsync(county => county.Id.Equals(countyId));

            var user = await dbContext.Users.SingleOrDefaultAsync(user => user.KingdomId.Equals(county.KingdomId) && user.Id.Equals(userId));

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public async Task SetWineConsumption(Guid countyId, int amount)
        {
            await dbContext.Counties.ForEachAsync(county =>
            {
                if (county.Id.Equals(countyId))
                {
                    county.WineConsumption = amount;
                }
            });

            await dbContext.SaveChangesAsync();
        }

        public async Task SpendResourcesAsync(Guid countyId, ResourcesDto resources)
        {
            await dbContext.Counties.ForEachAsync(county =>
            {
                if (county.Id.Equals(countyId))
                {
                    county.Wood -= resources.Wood;
                    county.Marble -= resources.Marble;
                    county.Wine -= resources.Wine;
                    county.Sulfur -= resources.Sulfur;
                }
                    
            });

            await dbContext.SaveChangesAsync();
        }

        public async Task TransferResourcesAsync(Guid sourceCountyId, Guid targetCountyId, ResourcesDto resources)
        {
            await dbContext.Counties.ForEachAsync(x =>
            {
                if (x.Id.Equals(sourceCountyId))
                {
                    x.Wood -= resources.Wood;
                    x.Marble -= resources.Marble;
                    x.Wine -= resources.Wine;
                    x.Sulfur -= resources.Sulfur;
                }
                if (x.Id.Equals(targetCountyId))
                {
                    x.Wood += resources.Wood;
                    x.Marble += resources.Marble;
                    x.Wine += resources.Wine;
                    x.Sulfur += resources.Sulfur;
                }
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
