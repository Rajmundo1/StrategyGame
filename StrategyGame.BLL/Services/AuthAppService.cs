using Microsoft.AspNetCore.Identity;
using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.Interfaces;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Exceptions;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Services
{
    public class AuthAppService : IAuthAppService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITokenService tokenService;
        private readonly IUnitOfWork unitOfWork;

        public AuthAppService(UserManager<User> userManager,
                                SignInManager<User> signInManager,
                                ITokenService tokenService,
                                IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.unitOfWork = unitOfWork;
        }

        public async Task<TokenDto> Login(LoginDto loginDto)
        {
            var result = await signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, true, false);
            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(loginDto.UserName);
                return new TokenDto
                {
                    AccessToken = await tokenService.CreateAccessToken(user)
                };
            }
            throw new AppException("Wrong username or password");
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<TokenDto> Register(RegisterDto registerDto)
        {

            var user = new User { UserName = registerDto.UserName};

            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                throw new ValidationAppException("User creation failed.", result.Errors.Select(e => e.Description));
            }
            await unitOfWork.SaveAsync();

            var storedUser = await userManager.FindByNameAsync(registerDto.UserName);

            var token = new TokenDto
                {
                    AccessToken = await tokenService.CreateAccessToken(storedUser) 
                };

            return token;
        }
    }
}
