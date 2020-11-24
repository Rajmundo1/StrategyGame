using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.FilterParameters;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.MODEL.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(Guid id);
        Task<PagedList<User>> GetPagedUsersAsync(PagingParameters pagingParameters);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<PagedList<User>> GetFilteredPagedUsersAsync(Expression<Func<User, bool>> filter, PagingParameters pagingParameters);
    }
}
