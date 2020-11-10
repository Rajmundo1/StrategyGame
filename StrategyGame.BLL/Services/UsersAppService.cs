﻿using AutoMapper;
using FluentValidation;
using LinqKit;
using Microsoft.AspNetCore.Identity;
using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.Interfaces;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Entities.Units;
using StrategyGame.MODEL.Exceptions;
using StrategyGame.MODEL.FilterParameters;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Services
{
    public class UsersAppService : IUsersAppService
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public UsersAppService(IUserRepository userRepository,
                                IUnitOfWork unitOfWork,
                                UserManager<User> userManager,
                                IMapper mapper)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<UserDto> CreateUserAsync(UserCreateDto userDto)
        {
            var user = new User { UserName = userDto.Username };

            var result = await userManager.CreateAsync(user, userDto.Password);
            if (!result.Succeeded)
            {
                throw new ValidationAppException("User creation failed.", result.Errors.Select(e => e.Description));
            }
            await unitOfWork.SaveAsync();

            var storedUser = await userManager.FindByNameAsync(userDto.Username);

            return mapper.Map<UserDto>(storedUser);
        }

        public async Task DeleteUserAsync(string id)
        {
            var userToDelete = await userManager.FindByIdAsync(id);
            await userManager.DeleteAsync(userToDelete);

            await unitOfWork.SaveAsync();
        }

        public async Task<PagedListDto<UserDto>> GetFilteredUsersAsync(PagingParametersDto pagingParametersDto, UserParametersDto parameters)
        {
            var pagingParameters = mapper.Map<PagingParameters>(pagingParametersDto);
            var userParameters = mapper.Map<UserParameters>(parameters);
            var filter = BuildFilterExpression(userParameters);

            var result = await userRepository.GetFilteredUseresAsync(filter, pagingParameters);

            var list = new List<UserDto>();
            foreach (var item in result)
            {
                list.Add(mapper.Map<UserDto>(item));
            }

            return new PagedListDto<UserDto>
            {
                Items = list,
                PaginationHeader = new PaginationHeader
                {
                    TotalCount = result.TotalCount,
                    CurrentPage = result.CurrentPage,
                    PageSize = result.PageSize,
                    TotalPages = result.TotalPages,
                    HasNext = result.HasNext,
                    HasPrevious = result.HasPrevious
                }
            };
        }

        public async Task<UserDto> GetUserAsync(string id)
        {
            var userToReturn = await userManager.FindByIdAsync(id);
            return mapper.Map<UserDto>(userToReturn);
        }

        public async Task<PagedListDto<UserDto>> GetUsersAsync(PagingParametersDto pagingParametersDto)
        {
            var pagingParameters = mapper.Map<PagingParameters>(pagingParametersDto);

            var result = await userRepository.GetUsersAsync(pagingParameters);

            var list = new List<UserDto>();
            foreach(var item in result)
            {
                list.Add(mapper.Map<UserDto>(item));
            }

            return new PagedListDto<UserDto>
            {
                Items = list,
                PaginationHeader = new PaginationHeader
                {
                    TotalCount = result.TotalCount,
                    CurrentPage = result.CurrentPage,
                    PageSize = result.PageSize,
                    TotalPages = result.TotalPages,
                    HasNext = result.HasNext,
                    HasPrevious = result.HasPrevious
                }
            };
        }

        private Expression<Func<User, bool>> BuildFilterExpression (UserParameters userParameters)
        {
            Expression<Func<User, bool>> filter = user => true;

            if (!string.IsNullOrEmpty(userParameters.Name))
            {
                filter = filter.And(user => user.UserName.Contains(userParameters.Name));
            }
            if(userParameters.MinScore != null)
            {
                filter = filter.And(user => user.Score >= userParameters.MinScore);
            }
            if(userParameters.MaxScore != null)
            {
                filter = filter.And(user => user.Score <= userParameters.MaxScore);
            }

            return filter;
        }
    }
}
