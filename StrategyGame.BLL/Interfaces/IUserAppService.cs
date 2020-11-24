using StrategyGame.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Interfaces
{
    public interface IUserAppService
    {
        Task<UserDto> CreateUserAsync(UserCreateDto userDto);
        Task<PagedListDto<UserDto>> GetUsersAsync(PagingParametersDto pagingParametersDto);
        Task<PagedListDto<UserDto>> GetFilteredUsersAsync(UserParametersDto parameters);
        Task<UserDto> GetUserAsync(Guid id);
        Task DeleteUserAsync(Guid id);
    }
}
