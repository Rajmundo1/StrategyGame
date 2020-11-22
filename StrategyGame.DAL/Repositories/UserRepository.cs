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

        public async Task<PagedList<User>> GetUsersAsync(PagingParameters pagingParameters)
        {
            return await dbContext.Users
                .Include(u => u.Kingdom)
                .ToPagedListAsync(pagingParameters.PageNumber, pagingParameters.PageSize);
        }

        public async Task<PagedList<User>> GetFilteredUseresAsync(Expression<Func<User, bool>> filter, PagingParameters pagingParameters)
        {
            return await dbContext.Users
                .Include(u => u.Kingdom)
                .Where(filter)
                .ToPagedListAsync(pagingParameters.PageNumber, pagingParameters.PageSize);
        }
        
    }
}
