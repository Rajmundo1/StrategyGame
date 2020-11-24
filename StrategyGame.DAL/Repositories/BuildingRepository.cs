using Microsoft.EntityFrameworkCore;
using StrategyGame.MODEL.Entities.Buildings;
using StrategyGame.MODEL.Enums;
using StrategyGame.MODEL.Exceptions;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.DAL.Repositories
{
    public class BuildingRepository: IBuildingRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BuildingRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Building>> GetBuildingsAsync(Guid countyId)
        {
            return await dbContext.Buildings
                .Include(building => building.BuildingSpecifics)
                .ThenInclude(specifics => specifics.BuildingLevels)
                .Where(building => building.CountyId.Equals(countyId))
                .ToListAsync();
        }

        public async Task<Building> GetBuildingAsync(Guid buildingId)
        {
            return await dbContext.Buildings.
                Include(building => building.BuildingSpecifics)
                .ThenInclude(spec => spec.BuildingLevels)
                .SingleAsync(building => building.Id.Equals(buildingId));
        }

        public async Task<Building> DevelopBuildingAsync(Guid buildingId)
        {
            var building = await dbContext.Buildings.SingleAsync(x => x.Id.Equals(buildingId));

            if(building.Status == BuildingStatus.NotBuilt)
            {
                await dbContext.Buildings.ForEachAsync(building =>
                {
                    if (building.Id.Equals(buildingId))
                    {
                        building.Status = BuildingStatus.Built;
                        building.Level = 1;
                    }
                });
            }
            else if (building.Status == BuildingStatus.Built)
            {
                await dbContext.Buildings.ForEachAsync(building =>
                {
                    if (building.Id.Equals(buildingId))
                        building.Level = building.Level + 1;
                });
            }

            await dbContext.SaveChangesAsync();

            return await dbContext.Buildings
                .Include(building => building.BuildingSpecifics)
                .ThenInclude(spec => spec.BuildingLevels)
                .SingleAsync(building => building.Id.Equals(buildingId));
        }
    }
}
