using StrategyGame.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Interfaces
{
    public interface IUserAppService
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<IEnumerable<UserDto>> GetFilteredUsersAsync(string userName);
        Task<UserDto> GetUserAsync(Guid id);
    }
}
