using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.Hubs;
using StrategyGame.BLL.Interfaces;
using StrategyGame.MODEL.DataTransferModels;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Exceptions;
using StrategyGame.MODEL.FilterParameters;
using StrategyGame.MODEL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.BLL.Services
{
    public class GameAppService : IGameAppService
    {

        private readonly IMapper mapper;
        private readonly IKingdomRepository kingdomRepository;
        private readonly ICountyRepository countyRepository;
        private readonly IUnitRepository unitRepository;
        private readonly IAttackRepository attackRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IGameRepository gameRepository;
        private readonly IUserRepository userRepository;
        private readonly IHubContext<MyHub> hubContext;
        private readonly IIdentityService identityService;

        public GameAppService(IMapper mapper,
                                IKingdomRepository kingdomRepository,
                                ICountyRepository countyRepository,
                                IUnitRepository unitRepository,
                                IAttackRepository attackRepository,
                                IUnitOfWork unitOfWork,
                                IGameRepository gameRepository,
                                IUserRepository userRepository,
                                IHubContext<MyHub> hubContext,
                                IIdentityService identityService)
        {
            this.mapper = mapper;
            this.kingdomRepository = kingdomRepository;
            this.countyRepository = countyRepository;
            this.unitRepository = unitRepository;
            this.attackRepository = attackRepository;
            this.unitOfWork = unitOfWork;
            this.gameRepository = gameRepository;
            this.userRepository = userRepository;
            this.hubContext = hubContext;
            this.identityService = identityService;
        }

        public async Task<IEnumerable<CountyDto>> GetAllCounties()
        {
            var currentUser = await identityService.GetCurrentUser();

            var counties = await countyRepository.GetAllCounties();

            var result = new List<CountyDto>();

            foreach(var county in counties)
            {
                if (!currentUser.KingdomId.Equals(county.KingdomId))
                {
                    var dummy = mapper.Map<CountyDto>(county);
                    dummy.Username = (await userRepository.GetUserByKingdomId(county.KingdomId)).UserName;
                    result.Add(dummy);
                }
            }

            return result;
        }

        public async Task<IEnumerable<CountyDto>> GetAllFilteredCounties(string userName)
        {
            var currentUser = await identityService.GetCurrentUser();

            var counties = await countyRepository.GetAllCounties();

            var result = new List<CountyDto>();

            foreach (var county in counties)
            {
                if (!currentUser.KingdomId.Equals(county.KingdomId))
                {
                    var dummy = mapper.Map<CountyDto>(county);
                    dummy.Username = (await userRepository.GetUserByKingdomId(county.KingdomId)).UserName;
                    if (dummy.Username.ToUpper().Contains(userName.ToUpper()))
                    {
                        result.Add(dummy);
                    }
                }
            }

            return result;
        }

        public async Task<IEnumerable<CountyDto>> GetCounties(Guid kingdomId)
        {
            var currentUser = await identityService.GetCurrentUser();
            if (!(await kingdomRepository.IsOwner(kingdomId, currentUser.Id)))
            {
                throw new AppException("You aren't the owner of that kingdom");
            }

            var counties = await countyRepository.GetCountiesByKingdomId(kingdomId);

            var result = new List<CountyDto>();

            foreach(var county in counties)
            {
                var dummy = mapper.Map<CountyDto>(county);
                dummy.Username = currentUser.UserName;
                result.Add(dummy);
            }

            return result;
        }

        public async Task<MainPageDto> GetCountyPage(Guid countyId)
        {
            var currentUser = await identityService.GetCurrentUser();
            if (!(await countyRepository.IsOwner(countyId, currentUser.Id)))
            {
                throw new AppException("You aren't the owner of that county");
            }

            var county = await countyRepository.GetCountyAsync(countyId);
            var kingdom = await kingdomRepository.GetKingdomAsync(county.KingdomId);

            var result = mapper.Map<MainPageDto>(county);

            var game = await gameRepository.GetGameByKingdomIdAsync(kingdom.Id);

            result.Round = game.Round;
            result.Gold = kingdom.Gold;
            result.ResearchPoint = kingdom.ResearchPoint;

            var buildingViewDtos = new List<BuildingViewDto>();

            foreach (var building in county.Buildings)
            {
                buildingViewDtos.Add(mapper.Map<BuildingViewDto>(building));
            }

            result.Buildings = buildingViewDtos;

            result.GoldPictureUrl = game.GoldPictureUrl;
            result.MarblePictureUrl = game.MarblePictureUrl;
            result.SulfurPictureUrl = game.SulfurPictureUrl;
            result.TechnologyPictureUrl = game.TechnologyPictureUrl;
            result.WinePictureUrl = game.WinePictureUrl;
            result.WoodPictureUrl = game.WoodPictureUrl;
            result.ForceLimitPictureUrl = game.ForceLimitPictureUrl;
            result.Username = currentUser.UserName;

            return result;
        }

        public async Task<MainPageDto> GetMainPage()
        {
            var currentUser = await identityService.GetCurrentUser();

            var kingdom = await kingdomRepository.GetKingdomAsync(currentUser.KingdomId);
            var countiesOfKingdom = kingdom.Counties.ToList();
            var county = await countyRepository.GetCountyAsync(countiesOfKingdom[0].Id);

            var result = mapper.Map<MainPageDto>(county);

            var game = await gameRepository.GetGameByKingdomIdAsync(kingdom.Id);

            result.Round = game.Round;
            result.Gold = kingdom.Gold;
            result.ResearchPoint = kingdom.ResearchPoint;

            var buildingViewDtos = new List<BuildingViewDto>();

            foreach (var building in county.Buildings)
            {
                buildingViewDtos.Add(mapper.Map<BuildingViewDto>(building));
            }

            result.Buildings = buildingViewDtos;

            result.GoldPictureUrl = game.GoldPictureUrl;
            result.MarblePictureUrl = game.MarblePictureUrl;
            result.SulfurPictureUrl = game.SulfurPictureUrl;
            result.TechnologyPictureUrl = game.TechnologyPictureUrl;
            result.WinePictureUrl = game.WinePictureUrl;
            result.WoodPictureUrl = game.WoodPictureUrl;
            result.ForceLimitPictureUrl = game.ForceLimitPictureUrl;
            result.Username = currentUser.UserName;

            return result;
        }

        public async Task NewCounty(Guid kingdomId, string countyName)
        {
            var currentUser = await identityService.GetCurrentUser();
            if (!(await kingdomRepository.IsOwner(kingdomId, currentUser.Id)))
            {
                throw new AppException("You aren't the owner of that kingdom");
            }

            var kingdom = await kingdomRepository.GetKingdomAsync(kingdomId);

            //check resources
            if(kingdom.Gold <= 10000)
            {
                throw new AppException("You don't have enough gold for a new county");
            }
            
            await countyRepository.NewCounty(kingdomId, countyName);
        }

        public async Task NewRound()
        {
            var rnd = new Random();

            //attackok lebonyolítása
            var attacks = await attackRepository.GetAllAttacks();
            attacks.OrderBy(attack => attack.TimeStamp);

            foreach(var attack in attacks)
            {
                var attackingArmy = attack.AttackerUnits.Units.ToList();
                var defendingArmy = attack.Defender.Units.Units.ToList();

                var attackingKingdom = await kingdomRepository.GetKingdomAsync(attack.Attacker.KingdomId);
                var attackingArmyBonus = attackingKingdom.Technologies.Sum(tech => tech.Specifics.AttackPowerBonus);

                var defendingKingdom = await kingdomRepository.GetKingdomAsync(attack.Defender.KingdomId);
                var defendingArmyBonus = defendingKingdom.Technologies.Sum(tech => tech.Specifics.DefensePowerBonus);

                while(attackingArmy.Count != 0 && defendingArmy.Count != 0)
                {
                    var currentAttacker = attackingArmy[rnd.Next(attackingArmy.Count-1)];
                    var currentDefender = defendingArmy[rnd.Next(defendingArmy.Count-1)];
                    if (currentAttacker.CurrentLevel.AttackPower >= currentDefender.CurrentLevel.DefensePower)
                    {
                        defendingArmy.Remove(currentDefender);
                        await unitRepository.RemoveUnitByIdAsync(currentDefender.Id);
                    }
                    else
                    {
                        attackingArmy.Remove(currentAttacker);
                        await unitRepository.RemoveUnitByIdAsync(currentAttacker.Id);
                    }
                }
                if (defendingArmy.Count == 0)
                {
                    await kingdomRepository.TranferGold(attack.Defender.KingdomId, attack.Attacker.KingdomId, Convert.ToInt32(attackingKingdom.Gold * 0.1));
                    await countyRepository.TransferResourcesAsync(attack.DefenderId, attack.AttackerId, new ResourcesDto
                    {
                        Wood = Convert.ToInt32(attack.Defender.Wood * 0.1),
                        Marble = Convert.ToInt32(attack.Defender.Marble * 0.1),
                        Wine = Convert.ToInt32(attack.Defender.Wine * 0.1),
                        Sulfur = Convert.ToInt32(attack.Defender.Sulfur * 0.1)
                    });
                }
            }

            //unitok hazaküldése
            attacks = await attackRepository.GetAllAttacks();
            foreach(var attack in attacks)
            {

                for(int i = attack.AttackerUnits.Units.Count() - 1; i >= 0; i--)
                //foreach(var unit in attack.AttackerUnits.Units)
                {
                    await unitRepository.MoveToUnitGroup(attack.AttackerUnits.Units.ElementAt(i).UnitSpecificsId,
                                                        attack.AttackerUnits.Units.ElementAt(i).Level,
                                                        attack.AttackerUnits.Units.ElementAt(i).Count,
                                                        attack.Attacker.Units.Id);

                    await unitRepository.RemoveUnitEntityByIdAsync(attack.AttackerUnits.Units.ElementAt(i).Id);
                }

                await unitRepository.RemoveUnitGroup(attack.AttackerUnits.Id);
                await attackRepository.RemoveAttack(attack.Id);

            }


            //búnuszok hozzáadása
            var kingdoms = await kingdomRepository.GetKingdomsAsync();
            foreach(var kingdom in kingdoms)
            {
                foreach(var county in kingdom.Counties)
                {
                    county.BasePopulation += county.PopulationGrowth;

                    county.Wood += county.WoodProduction + county.WoodProductionBonus;
                    county.Marble += county.MarbleProduction + county.MarbleProductionBonus;
                    county.Wine += county.WineProduction + county.WineProductionBonus;
                    county.Sulfur += county.SulfurProduction + county.SulfurProductionBonus;

                    kingdom.Gold += county.GoldIncome + county.GoldIncomeBonus;
                }

                kingdom.ResearchPoint += kingdom.GlobalResearchOutput;
            }
            //költségek levonása
            foreach (var kingdom in kingdoms)
            {
                foreach (var county in kingdom.Counties)
                {
                    if(county.Wine - county.WineConsumption < 0)
                    {
                        county.WineConsumption = 0;
                    }
                    else
                    {
                        county.Wine -= county.WineConsumption;
                    }

                    for(int i = county.Units.Units.Count() - 1; i >= 0; i--)
                    {
                        var unit = county.Units.Units.ElementAt(i);
                        if (kingdom.Gold - unit.CurrentLevel.GoldUpkeep < 0 ||
                            county.Wood - unit.CurrentLevel.WoodUpkeep < 0 ||
                            county.Marble - unit.CurrentLevel.MarbleUpkeep < 0 ||
                            county.Wine - unit.CurrentLevel.WineUpkeep < 0 ||
                            county.Sulfur - unit.CurrentLevel.SulfurUpkeep < 0)
                        {
                            await unitRepository.RemoveUnitByIdAsync(unit.Id);
                        }
                        else
                        {
                            kingdom.Gold -= unit.CurrentLevel.GoldUpkeep;
                            county.Wood -= unit.CurrentLevel.WoodUpkeep;
                            county.Marble -= unit.CurrentLevel.MarbleUpkeep;
                            county.Wine -= unit.CurrentLevel.WineUpkeep;
                            county.Sulfur -= unit.CurrentLevel.SulfurUpkeep;
                        }   
                        
                    }
                }
            }

            //scoreboard placement

            var users = (await userRepository
                .GetAllUsersAsync())
                .OrderByDescending(user => user.Score);

            var place = 1;

            foreach(var user in users)
            {
                user.ScoreboardPlace = place++;
            }

            var games = await gameRepository.GetAllGames();
            foreach(var game in games)
            {
                game.Round += 1;
            }

            await unitOfWork.SaveAsync();

            await hubContext.Clients.All.SendAsync("NewRound");
        }

        public async Task SetWineConsumption(Guid countyId, int amount)
        {
            var currentUser = await identityService.GetCurrentUser();
            if (!(await countyRepository.IsOwner(countyId, currentUser.Id)))
            {
                throw new AppException("You aren't the owner of that county");
            }

            await countyRepository.SetWineConsumption(countyId, amount);
        }
    }
}
