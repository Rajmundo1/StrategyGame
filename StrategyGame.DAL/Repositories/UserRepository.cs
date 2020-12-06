using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StrategyGame.DAL.Extensions;
using StrategyGame.MODEL;
using StrategyGame.MODEL.DataTransferModels;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Entities.Buildings;
using StrategyGame.MODEL.Entities.Technologies;
using StrategyGame.MODEL.Entities.Units;
using StrategyGame.MODEL.Enums;
using StrategyGame.MODEL.FilterParameters;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetFilteredUsersAsync(string userName)
        {

            return await dbContext.Users
                .Include(u => u.Kingdom)
                .ThenInclude(k => k.Technologies)
                .Include(u => u.Kingdom)
                .ThenInclude(k => k.Counties)
                .ThenInclude(c => c.Buildings)
                .ThenInclude(b => b.BuildingSpecifics)
                .ThenInclude(bsp => bsp.BuildingLevels)
                .Where(u => u.UserName.ToUpper().Contains(userName.ToUpper()))
                .OrderBy(u => u.ScoreboardPlace)
                .ToListAsync();
        }

        public async Task<User> GetUserAsync(Guid id)
        {
            return await dbContext.Users
            .Include(u => u.Kingdom)
            .ThenInclude(k => k.Technologies)
            .Include(u => u.Kingdom)
            .ThenInclude(k => k.Counties)
            .ThenInclude(c => c.Buildings)
            .ThenInclude(b => b.BuildingSpecifics)
            .ThenInclude(bsp => bsp.BuildingLevels)
            .SingleAsync(u => u.Id.Equals(id.ToString()));
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await dbContext.Users
            .Include(u => u.Kingdom)
            .ThenInclude(k => k.Technologies)
            .Include(u => u.Kingdom)
            .ThenInclude(k => k.Counties)
            .ThenInclude(c => c.Buildings)
            .ThenInclude(b => b.BuildingSpecifics)
            .ThenInclude(bsp => bsp.BuildingLevels)
            .OrderBy(u => u.ScoreboardPlace)
            .ToListAsync();
        }

        public async Task Register(RegisterData registerData)
        {
            var passwordHasher = new PasswordHasher<IdentityUser>();
            var userId = Guid.NewGuid();
            var kingdomId = Guid.NewGuid();
            var countyId = Guid.NewGuid();
            var unitGroupId = Guid.NewGuid();

            var newTechnologies = new List<Technology>
            {
                new Technology
                {
                    Id = Guid.NewGuid(),
                    KingdomId = kingdomId,
                    TechnologySpecificsId = Guid.Parse("a6336474-fa17-43ba-a5c6-7fee92ab15b7"),
                    Status = ResearchStatus.UnResearched
                },
                new Technology
                {
                    Id = Guid.NewGuid(),
                    KingdomId = kingdomId,
                    TechnologySpecificsId = Guid.Parse("f7f7f6a9-1ce5-4051-82b0-a55fb19d901c"),
                    Status = ResearchStatus.UnResearched
                },
                new Technology
                {
                    Id = Guid.NewGuid(),
                    KingdomId = kingdomId,
                    TechnologySpecificsId = Guid.Parse("93ad7e45-7071-48d5-a5df-c5eb21bb35da"),
                    Status = ResearchStatus.UnResearched
                },
                new Technology
                {
                    Id = Guid.NewGuid(),
                    KingdomId = kingdomId,
                    TechnologySpecificsId = Guid.Parse("4e9f32b6-2621-4f7c-a939-f4d1a1a2daae"),
                    Status = ResearchStatus.UnResearched
                },
            };

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
                    CountyId = Guid.Parse("217f6d72-a33e-4612-b164-f1bbd5db94c2"),
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
                Name = registerData.CountyName,
                TaxRate = 1.0,
                WineConsumption = 0,
                Wood = 5000,
                Marble = 5000,
                Wine = 2000,
                Sulfur = 1000,
                BasePopulation = 200,
            };

            var newKingdom = new Kingdom
            {
                Id = kingdomId,
                Gold = 3000,
                ResearchPoint = 2000,
            };

            var newUser = new User
            {
                Id = userId.ToString(),
                UserName = registerData.UserName,
                NormalizedUserName = registerData.UserName.Normalize(),
                GameId = Guid.Parse("1bb1f3c1-8c10-439c-8dcb-7f8cc1f8044e"),
                KingdomId = kingdomId,
            };
            newUser.PasswordHash = passwordHasher.HashPassword(newUser, registerData.Password);

            await dbContext.Kingdoms.AddAsync(newKingdom);
            await dbContext.SaveChangesAsync();

            await dbContext.Counties.AddAsync(newCounty);
            await dbContext.SaveChangesAsync();

            await dbContext.UnitGroups.AddAsync(newUnitGroup);
            await dbContext.SaveChangesAsync();

            await dbContext.Buildings.AddRangeAsync(newBuildings);
            await dbContext.SaveChangesAsync();

            await dbContext.Technologies.AddRangeAsync(newTechnologies);
            await dbContext.SaveChangesAsync();

            await dbContext.Users.AddAsync(newUser);
            await dbContext.SaveChangesAsync();

            //calculate rankings
            var users = (await 
                GetAllUsersAsync())
                .OrderByDescending(user => user.Score);

            var place = 1;

            foreach (var user in users)
            {
                user.ScoreboardPlace = place++;
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByRefreshToken(string refreshToken)
        {
            return await dbContext.Users
                .SingleAsync(user => user.RefreshToken.Equals(refreshToken));
        }

        public async Task RemoveRefreshToken(string userId)
        {
            await dbContext.Users
                .ForEachAsync(user =>
                {
                    if (user.Id.Equals(userId))
                    {
                        user.RefreshToken = null;
                    }
                });

            await dbContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByKingdomId(Guid kingdomId)
        {
            return await dbContext.Users
                .SingleAsync(user => user.KingdomId.Equals(kingdomId));
        }
    }
}
