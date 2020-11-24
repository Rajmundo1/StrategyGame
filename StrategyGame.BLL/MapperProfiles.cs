using AutoMapper;
using StrategyGame.BLL.Dtos;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Entities.Buildings;
using StrategyGame.MODEL.Entities.Technologies;
using StrategyGame.MODEL.Entities.Units;
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
            CreateMap<User, UserDto>();
            CreateMap<PagingParametersDto, PagingParameters>();
            CreateMap<UserParametersDto, UserParameters>();
            CreateMap<Building, BuildingDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.BuildingSpecifics.Description))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.BuildingSpecifics.ImageUrl))
                .ForMember(dest => dest.MaxLevel, opt => opt.MapFrom(src => src.BuildingSpecifics.MaxLevel))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BuildingSpecifics.Name));

            CreateMap<Building, BuildingDetailDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.BuildingSpecifics.Description))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.BuildingSpecifics.ImageUrl))
                .ForMember(dest => dest.MaxLevel, opt => opt.MapFrom(src => src.BuildingSpecifics.MaxLevel))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.BuildingSpecifics.Name))
                .ForMember(dest => dest.MarbleCost, opt => opt.MapFrom(src => src.CurrentLevel.MarbleCost))
                .ForMember(dest => dest.MarbleProduction, opt => opt.MapFrom(src => src.CurrentLevel.MarbleProduction))
                .ForMember(dest => dest.PopulationBonus, opt => opt.MapFrom(src => src.CurrentLevel.PopulationBonus))
                .ForMember(dest => dest.ResearchOutPut, opt => opt.MapFrom(src => src.CurrentLevel.ResearchOutPut))
                .ForMember(dest => dest.SulfurCost, opt => opt.MapFrom(src => src.CurrentLevel.SulfurCost))
                .ForMember(dest => dest.SulfurProduction, opt => opt.MapFrom(src => src.CurrentLevel.SulfurProduction))
                .ForMember(dest => dest.WineCost, opt => opt.MapFrom(src => src.CurrentLevel.WineCost))
                .ForMember(dest => dest.WineProduction, opt => opt.MapFrom(src => src.CurrentLevel.WineProduction))
                .ForMember(dest => dest.WoodCost, opt => opt.MapFrom(src => src.CurrentLevel.WoodCost))
                .ForMember(dest => dest.WoodProduction, opt => opt.MapFrom(src => src.CurrentLevel.WoodProduction))
                .ForMember(dest => dest.ForceLimitBonus, opt => opt.MapFrom(src => src.CurrentLevel.ForceLimitBonus))
                .ForMember(dest => dest.GoldCost, opt => opt.MapFrom(src => src.CurrentLevel.GoldCost));

            CreateMap<BuildingLevel, BuildingNextLevelDto>()
                .ForMember(dest => dest.MarbleCost, opt => opt.MapFrom(src => src.MarbleCost))
                .ForMember(dest => dest.MarbleProduction, opt => opt.MapFrom(src => src.MarbleProduction))
                .ForMember(dest => dest.PopulationBonus, opt => opt.MapFrom(src => src.PopulationBonus))
                .ForMember(dest => dest.ResearchOutPut, opt => opt.MapFrom(src => src.ResearchOutPut))
                .ForMember(dest => dest.SulfurCost, opt => opt.MapFrom(src => src.SulfurCost))
                .ForMember(dest => dest.SulfurProduction, opt => opt.MapFrom(src => src.SulfurProduction))
                .ForMember(dest => dest.WineCost, opt => opt.MapFrom(src => src.WineCost))
                .ForMember(dest => dest.WineProduction, opt => opt.MapFrom(src => src.WineProduction))
                .ForMember(dest => dest.WoodCost, opt => opt.MapFrom(src => src.WoodCost))
                .ForMember(dest => dest.WoodProduction, opt => opt.MapFrom(src => src.WoodProduction))
                .ForMember(dest => dest.ForceLimitBonus, opt => opt.MapFrom(src => src.ForceLimitBonus))
                .ForMember(dest => dest.GoldCost, opt => opt.MapFrom(src => src.GoldCost));

            CreateMap<Technology, TechnologyDetailDto>()
                .ForMember(dest => dest.AttackPowerBonus, opt => opt.MapFrom(src => src.Specifics.AttackPowerBonus))
                .ForMember(dest => dest.DefensePowerBonus, opt => opt.MapFrom(src => src.Specifics.DefensePowerBonus))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Specifics.Description))
                .ForMember(dest => dest.GoldBonus, opt => opt.MapFrom(src => src.Specifics.GoldBonus))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Specifics.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.Specifics.PictureUrl))
                .ForMember(dest => dest.ResearchBonus, opt => opt.MapFrom(src => src.Specifics.ResearchBonus))
                .ForMember(dest => dest.StoneBonus, opt => opt.MapFrom(src => src.Specifics.StoneBonus))
                .ForMember(dest => dest.SulfurBonus, opt => opt.MapFrom(src => src.Specifics.SulfurBonus))
                .ForMember(dest => dest.WineBonus, opt => opt.MapFrom(src => src.Specifics.WineBonus))
                .ForMember(dest => dest.WoodBonus, opt => opt.MapFrom(src => src.Specifics.WoodBonus));

            CreateMap<Technology, TechnologyDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Specifics.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Specifics.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.Specifics.PictureUrl))
                .ForMember(dest => dest.ResearchPointCost, opt => opt.MapFrom(src => src.Specifics.ResearchPointCost));

            CreateMap<UnitLevel, UnitNextLevelDto>();
            CreateMap<UnitLevel, UnitDetailsDto>();

            CreateMap<County, MainPageDto>()
                .ForMember(dest => dest.CurrentCountyName, opt => opt.MapFrom(src => src.Name));
            CreateMap<Building, BuildingViewDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.BuildingSpecifics.ImageUrl));
        }
    }
}
