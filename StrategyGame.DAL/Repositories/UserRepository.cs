using Microsoft.EntityFrameworkCore;
using StrategyGame.DAL.Extensions;
using StrategyGame.MODEL;
using StrategyGame.MODEL.Entities;
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

        public async Task<PagedList<User>> GetPagedUsersAsync(PagingParameters pagingParameters)
        {
            return await dbContext.Users
                .Include(u => u.Kingdom)
                .ThenInclude(k => k.Technologies)
                .Include(u => u.Kingdom)
                .ThenInclude(k => k.Counties)
                .ThenInclude(c => c.Buildings)
                .ThenInclude(b => b.BuildingSpecifics)
                .ThenInclude(bsp => bsp.BuildingLevels)
                .ToPagedListAsync(pagingParameters.PageNumber, pagingParameters.PageSize);
        }

        public async Task<PagedList<User>> GetFilteredPagedUsersAsync(Expression<Func<User, bool>> filter, PagingParameters pagingParameters)
        {

            return await dbContext.Users
                .Include(u => u.Kingdom)
                .ThenInclude(k => k.Technologies)
                .Include(u => u.Kingdom)
                .ThenInclude(k => k.Counties)
                .ThenInclude(c => c.Buildings)
                .ThenInclude(b => b.BuildingSpecifics)
                .ThenInclude(bsp => bsp.BuildingLevels)
                .Where(filter)
                .ToPagedListAsync(pagingParameters.PageNumber, pagingParameters.PageSize);
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
            .ToListAsync();
        }
    }
}
