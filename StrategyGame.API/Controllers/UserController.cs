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
    [Authorize]
    public class UserController : StrategyGameControllerBase
    {
        private readonly IUserAppService userAppService;

        public UserController(IUserAppService userAppService)
        {
            this.userAppService = userAppService;
        }

        [HttpGet("users")]
        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return await userAppService.GetUsersAsync();
        }

        [HttpGet("filteredUsers")]
        public async Task<IEnumerable<UserDto>> GetFilteredUsersAsync([FromQuery] string userName)
        {
            return await userAppService.GetFilteredUsersAsync(userName);
        }

        [HttpGet("user/{id}")]
        public async Task<UserDto> GetUserAsync([FromRoute] Guid id)
        {
            return await userAppService.GetUserAsync(id);
        }
    }
}
