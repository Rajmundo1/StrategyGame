using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StrategyGame.API.Common;
using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.Interfaces;
using StrategyGame.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyGame.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    //[Authorize]
    public class UserController : StrategyGameControllerBase
    {
        private readonly IUserAppService userAppService;

        public UserController(IUserAppService userAppService)
        {
            this.userAppService = userAppService;
        }

        [HttpGet("users")]
        public async Task<PagedListDto<UserDto>> GetUsersAsync([FromQuery] PagingParametersDto pagingParametersDto)
        {
            return await userAppService.GetUsersAsync(pagingParametersDto);
        }

        [HttpGet("filteredUsers")]
        public async Task<PagedListDto<UserDto>> GetFilteredUsersAsync([FromQuery] UserParametersDto userParametersDto)
        {
            return await userAppService.GetFilteredUsersAsync(userParametersDto);
        }

        [HttpGet("user/{id}")]
        public async Task<UserDto> GetUserAsync([FromRoute] Guid id)
        {
            return await userAppService.GetUserAsync(id);
        }

        [HttpPost("delete/{id}")]
        public async Task DeleteUserAsync([FromRoute] Guid id)
        {
            await userAppService.DeleteUserAsync(id);
        }

        [HttpPost("newUser")]
        public async Task<UserDto> CreateUserAsync([FromBody] UserCreateDto userCreateDto)
        {
            return await userAppService.CreateUserAsync(userCreateDto);
        }


    }
}
