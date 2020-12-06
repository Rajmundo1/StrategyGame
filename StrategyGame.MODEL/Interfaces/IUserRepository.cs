using StrategyGame.MODEL.DataTransferModels;
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
        Task Register(RegisterData registerData);
        Task<User> GetUserAsync(Guid id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetFilteredUsersAsync(string userName);
        Task<User> GetUserByRefreshToken(string refreshToken);
        Task RemoveRefreshToken(string userId);
        Task<User> GetUserByKingdomId(Guid kingdomId);
    }
}
