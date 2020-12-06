﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.Interfaces;
using StrategyGame.MODEL.DataTransferModels;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Exceptions;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Services
{
    public class AuthAppService : IAuthAppService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ITokenAppService tokenService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;

        public AuthAppService(UserManager<User> userManager,
                                SignInManager<User> signInManager,
                                ITokenAppService tokenService,
                                IUnitOfWork unitOfWork,
                                IUserRepository userRepository,
                                IMapper mapper,
                                IIdentityService identityService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tokenService = tokenService;
            this.unitOfWork = unitOfWork;
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.identityService = identityService;
        }

        public async Task<TokenDto> Login(LoginDto loginDto)
        {
            var result = await signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, true, false);
            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(loginDto.UserName);
                return new TokenDto
                {
                    AccessToken = await tokenService.CreateNormalAccessToken(user),
                    RefreshToken = await tokenService.CreateNormalRefreshTokenAsync(user)
                };
            }
            throw new AppException("Wrong username or password");
        }

        public async Task Logout()
        {
            var userId = await identityService.GetCurrentUserId();
            await signInManager.SignOutAsync();
            await userRepository.RemoveRefreshToken(userId.ToString());
        }

        public async Task<TokenDto> Register(RegisterDto registerDto)
        {
            var registerData = mapper.Map<RegisterData>(registerDto);
            await userRepository.Register(registerData);

            var storedUser = await userManager.FindByNameAsync(registerDto.UserName);

            return new TokenDto
            {
                AccessToken = await tokenService.CreateNormalAccessToken(storedUser),
                RefreshToken = await tokenService.CreateNormalRefreshTokenAsync(storedUser)
            };
        }

        public async Task<TokenDto> RenewToken(string refreshToken)
        {
            var user = await userRepository.GetUserByRefreshToken(refreshToken);

            if(user == null)
            {
                throw new AppException("There is no user with that specific refresh token");
            }
            return new TokenDto
            {
                AccessToken = await tokenService.CreateNormalAccessToken(user),
                RefreshToken = await tokenService.CreateNormalRefreshTokenAsync(user)
            };
        }

    }
}
