using Microsoft.AspNetCore.Mvc;
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
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
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

        [HttpGet("{id}")]
        public async Task<UserDto> GetUserAsync([FromRoute] string id)
        {
            return await userAppService.GetUserAsync(id);
        }

        [HttpPost("{id}")]
        public async Task DeleteUserAsync([FromRoute] string id)
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
