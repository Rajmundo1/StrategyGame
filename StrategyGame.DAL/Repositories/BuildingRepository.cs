using Microsoft.EntityFrameworkCore;
using StrategyGame.MODEL.Entities.Buildings;
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
                .Where(building => building.CountyId.Equals(countyId))
                .ToListAsync();
        }

        public async Task<Building> GetBuilding(Guid buildingId)
        {
            return await dbContext.Buildings.SingleAsync(building => building.Id.Equals(buildingId));
        }

        public async Task<Building> DevelopBuilding(Guid buildingId)
        {
            await dbContext.Buildings.ForEachAsync(building => 
                                                    {
                                                        if (building.Id.Equals(buildingId))
                                                            building.BuildingSpecifics.Level++; 
                                                    });

            return await dbContext.Buildings.SingleAsync(building => building.Id.Equals(buildingId));
        }
    }
}
