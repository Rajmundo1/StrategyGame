using StrategyGame.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Interfaces
{
    public interface ITokenAppService
    {
        Task<string> CreateNormalAccessToken(User user);
        Task<string> CreateNormalRefreshTokenAsync(User user);
        Task RemoveRefreshToken(string userId);
    }
}
