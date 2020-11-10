using StrategyGame.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Interfaces
{
    public interface IUsersAppService
    {
        Task<UserDto> CreateUserAsync(UserCreateDto userDto);
        Task<PagedListDto<UserDto>> GetUsersAsync(PagingParametersDto pagingParametersDto);
        Task<PagedListDto<UserDto>> GetFilteredUsersAsync(PagingParametersDto pagingParametersDto, UserParametersDto parameters);
        Task<UserDto> GetUserAsync(string id);
        Task DeleteUserAsync(string id);
    }
}
