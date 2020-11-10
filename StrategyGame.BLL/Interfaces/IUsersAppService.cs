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
        Task<UserDto> GetUserAsync(Guid id);
        Task DeleteUserAsync(Guid id);
    }
}
