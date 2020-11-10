using AutoMapper;
using StrategyGame.BLL.Dtos;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.FilterParameters;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL
{
    public class MapperProfiles: Profile
    {
        public MapperProfiles()
        {
            CreateMap<UserDto, User>();
            CreateMap<PagingParameters, PagingParametersDto>();
            CreateMap<UserParameters, UserParametersDto>();
        }
    }
}
