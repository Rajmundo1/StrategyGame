using AutoMapper;
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
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserAppService(IUserRepository userRepository,
                                IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<PagedListDto<UserDto>> GetFilteredUsersAsync(UserParametersDto parameters)
        {
            var pagingParameters = mapper.Map<PagingParameters>(parameters);
            var userParameters = mapper.Map<UserParameters>(parameters);
            var filter = BuildFilterExpression(userParameters);

            var result = await userRepository.GetFilteredPagedUsersAsync(filter, pagingParameters);

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

        public async Task<UserDto> GetUserAsync(Guid id)
        {
            var userToReturn = await userRepository.GetUserAsync(id);
            return mapper.Map<UserDto>(userToReturn);
        }

        public async Task<PagedListDto<UserDto>> GetUsersAsync(PagingParametersDto pagingParametersDto)
        {
            var pagingParameters = mapper.Map<PagingParameters>(pagingParametersDto);

            var result = await userRepository.GetPagedUsersAsync(pagingParameters);

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
            if(userParameters.ScoreboardPlace != null)
            {
                filter = filter.And(user => user.ScoreboardPlace == userParameters.ScoreboardPlace);
            }

            return filter;
        }
    }
}
