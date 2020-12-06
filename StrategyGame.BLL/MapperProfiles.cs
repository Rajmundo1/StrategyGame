using AutoMapper;
using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.HelperClasses;
using StrategyGame.MODEL.DataTransferModels;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Entities.Buildings;
using StrategyGame.MODEL.Entities.Technologies;
using StrategyGame.MODEL.Entities.Units;
using StrategyGame.MODEL.FilterParameters;
using System;
using System.Collections.Generic;
using System.Linq;
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
                .ForMember(dest => dest.WoodBonus, opt => opt.MapFrom(src => src.Specifics.WoodBonus))
                .ForMember(dest => dest.ResearchPointCost, opt => opt.MapFrom(src => src.Specifics.ResearchPointCost));

            CreateMap<Technology, TechnologyDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Specifics.Description))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Specifics.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.Specifics.PictureUrl))
                .ForMember(dest => dest.ResearchPointCost, opt => opt.MapFrom(src => src.Specifics.ResearchPointCost));

            CreateMap<UnitLevel, UnitNextLevelDto>();
            CreateMap<UnitLevel, UnitDetailsDto>();

            CreateMap<County, MainPageDto>()
                .ForMember(dest => dest.CurrentCountyName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CurrentCountyId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CurrentKingdomId, opt => opt.MapFrom(src => src.KingdomId));

            CreateMap<Building, BuildingViewDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.BuildingSpecifics.ImageUrl));

            CreateMap<Attack, AttackDto>()
                .ForMember(dest => dest.AttackerCountyId, opt => opt.MapFrom(src => src.AttackerId))
                .ForMember(dest => dest.DefenderCountyId, opt => opt.MapFrom(src => src.DefenderId))
                .ForMember(dest => dest.DefenderCountyName, opt => opt.MapFrom(src => src.Defender.Name));

            CreateMap<RegisterDto, RegisterData>();

            CreateMap<County, CountyDto>()
                .ForMember(dest => dest.CountyName, opt => opt.MapFrom(src => src.Name));

            CreateMap<UnitSpecificsAndLevel, UnitSpecificsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UnitSpecifics.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UnitSpecifics.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.UnitSpecifics.ImageUrl))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level))
                .ForMember(dest => dest.AttackPower, opt => opt.MapFrom(src => src.UnitSpecifics.UnitLevels.Single(u => u.Level == src.Level).AttackPower))
                .ForMember(dest => dest.DefensePower, opt => opt.MapFrom(src => src.UnitSpecifics.UnitLevels.Single(u => u.Level == src.Level).DefensePower))
                .ForMember(dest => dest.ForceLimit, opt => opt.MapFrom(src => src.UnitSpecifics.UnitLevels.Single(u => u.Level == src.Level).ForceLimit))
                .ForMember(dest => dest.WoodCost, opt => opt.MapFrom(src => src.UnitSpecifics.UnitLevels.Single(u => u.Level == src.Level).WoodCost))
                .ForMember(dest => dest.MarbleCost, opt => opt.MapFrom(src => src.UnitSpecifics.UnitLevels.Single(u => u.Level == src.Level).MarbleCost))
                .ForMember(dest => dest.WineCost, opt => opt.MapFrom(src => src.UnitSpecifics.UnitLevels.Single(u => u.Level == src.Level).WineCost))
                .ForMember(dest => dest.SulfurCost, opt => opt.MapFrom(src => src.UnitSpecifics.UnitLevels.Single(u => u.Level == src.Level).SulfurCost))
                .ForMember(dest => dest.GoldCost, opt => opt.MapFrom(src => src.UnitSpecifics.UnitLevels.Single(u => u.Level == src.Level).GoldCost))
                .ForMember(dest => dest.WoodUpkeep, opt => opt.MapFrom(src => src.UnitSpecifics.UnitLevels.Single(u => u.Level == src.Level).WoodUpkeep))
                .ForMember(dest => dest.MarbleUpkeep, opt => opt.MapFrom(src => src.UnitSpecifics.UnitLevels.Single(u => u.Level == src.Level).MarbleUpkeep))
                .ForMember(dest => dest.WineUpkeep, opt => opt.MapFrom(src => src.UnitSpecifics.UnitLevels.Single(u => u.Level == src.Level).WineUpkeep))
                .ForMember(dest => dest.SulfurUpkeep, opt => opt.MapFrom(src => src.UnitSpecifics.UnitLevels.Single(u => u.Level == src.Level).SulfurUpkeep))
                .ForMember(dest => dest.GoldUpkeep, opt => opt.MapFrom(src => src.UnitSpecifics.UnitLevels.Single(u => u.Level == src.Level).GoldUpkeep));

            CreateMap<Unit, UnitDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UnitSpecifics.Name))
                .ForMember(dest => dest.MaxLevel, opt => opt.MapFrom(src => src.UnitSpecifics.MaxLevel))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.UnitSpecifics.ImageUrl));
        }
    }
}
