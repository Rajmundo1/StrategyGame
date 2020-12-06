using Microsoft.EntityFrameworkCore;
using StrategyGame.MODEL.DataTransferModels;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Entities.Buildings;
using StrategyGame.MODEL.Entities.Technologies;
using StrategyGame.MODEL.Entities.Units;
using StrategyGame.MODEL.Enums;
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
        private readonly IUserRepository userRepository;

        public CountyRepository(ApplicationDbContext dbContext,
                                IUserRepository userRepository)
        {
            this.dbContext = dbContext;
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<County>> GetAllCounties()
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
                .ToListAsync();
        }

        public async Task<IEnumerable<County>> GetCountiesByKingdomId(Guid kingdomId)
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
                .Where(x => x.KingdomId.Equals(kingdomId))
                .ToListAsync();
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

        public async Task NewCounty(Guid kingdomId, string countyName)
        {
            var countyId = Guid.NewGuid();
            var unitGroupId = Guid.NewGuid();

            var newBuildings = new List<Building>
            {
                new Building
                {
                    Id = Guid.NewGuid(),
                    BuildingSpecificsId = Guid.Parse("e2bfc4a7-d73f-4a2e-b91f-209c08a3f14f"),
                    CountyId = countyId,
                    Level = 1,
                    Status = BuildingStatus.NotBuilt
                },
                new Building
                {
                    Id = Guid.NewGuid(),
                    BuildingSpecificsId = Guid.Parse("1d203260-0928-47b6-9d10-5e4cf0c70265"),
                    CountyId = countyId,
                    Level = 1,
                    Status = BuildingStatus.NotBuilt
                },
                new Building
                {
                    Id = Guid.NewGuid(),
                    BuildingSpecificsId = Guid.Parse("4db1c8d2-b2b0-49a9-b8a5-8f9d5bbecddb"),
                    CountyId = countyId,
                    Level = 1,
                    Status = BuildingStatus.NotBuilt
                },
                new Building
                {
                    Id = Guid.NewGuid(),
                    BuildingSpecificsId = Guid.Parse("d02e3c9c-f26c-4136-a904-27ad074fa456"),
                    CountyId = countyId,
                    Level = 1,
                    Status = BuildingStatus.NotBuilt
                },
                new Building
                {
                    Id = Guid.NewGuid(),
                    BuildingSpecificsId = Guid.Parse("598fd678-5915-4c88-80d8-ff389c8278f9"),
                    CountyId = countyId,
                    Level = 1,
                    Status = BuildingStatus.NotBuilt
                },
                new Building
                {
                    Id = Guid.NewGuid(),
                    BuildingSpecificsId = Guid.Parse("3a8ffb5d-6edb-4908-a72e-3d268128efee"),
                    CountyId = countyId,
                    Level = 1,
                    Status = BuildingStatus.NotBuilt
                },
            };

            var newUnitGroup = new UnitGroup
            {
                Id = unitGroupId,
                AttackId = null,
                CountyId = countyId,
            };

            var newCounty = new County
            {
                Id = countyId,
                KingdomId = kingdomId,
                Name = countyName,
                TaxRate = 1.0,
                WineConsumption = 0,
                Wood = 5000,
                Marble = 5000,
                Wine = 2000,
                Sulfur = 1000,
                BasePopulation = 200,
            };


            await dbContext.Counties.AddAsync(newCounty);
            await dbContext.SaveChangesAsync();

            await dbContext.UnitGroups.AddAsync(newUnitGroup);
            await dbContext.SaveChangesAsync();

            await dbContext.Buildings.AddRangeAsync(newBuildings);
            await dbContext.SaveChangesAsync();

            //calculate rankings
            var users = (await userRepository
                .GetAllUsersAsync())
                .OrderByDescending(user => user.Score);

            var place = 1;

            foreach (var user in users)
            {
                user.ScoreboardPlace = place++;
            }

            await dbContext.SaveChangesAsync();
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
