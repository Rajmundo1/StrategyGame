using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.FilterParameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.MODEL.Interfaces
{
    public interface IUserRepository
    {
        Task<PagedList<User>> GetUsersAsync(PagingParameters pagingParameters);
        Task<User> FindAsync(string id);
    }
}
