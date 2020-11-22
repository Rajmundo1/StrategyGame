using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.Interfaces;
using StrategyGame.BLL.ValidationDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly IAuthAppService authService;

        public AuthController(IAuthAppService authService)
        {
            this.authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        public async Task<TokenDto> Login([FromBody] LoginDto loginDto)
        {
            return await authService.Login(loginDto);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<TokenDto> Register([FromBody] RegisterDto registerDto)
        {
            return await authService.Register(registerDto);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task Logout()
        {
            await authService.Logout();
        }
    }
}
