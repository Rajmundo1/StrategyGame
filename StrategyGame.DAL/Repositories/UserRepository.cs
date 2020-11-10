using StrategyGame.DAL.Extensions;
using StrategyGame.MODEL;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.FilterParameters;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<User> FindAsync(string id)
        {
            return await dbContext.Users.FindAsync(id);
        }

        public async Task<PagedList<User>> GetUsersAsync(PagingParameters pagingParameters)
        {
            return await dbContext.Users.ToPagedListAsync(pagingParameters.PageNumber, pagingParameters.PageSize);
        }
    }
}
