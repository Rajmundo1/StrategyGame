using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Services
{
    public class UsersAppService : IUsersAppService
    {
        public Task<UserDto> CreateUserAsync(UserCreateDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetUserAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedListDto<UserDto>> GetUsersAsync(PagingParametersDto pagingParametersDto)
        {
            throw new NotImplementedException();
        }
    }
}
