using AutoMapper;
using StrategyGame.BLL.Dtos;
using StrategyGame.BLL.Interfaces;
using StrategyGame.MODEL.DataTransferModels;
using StrategyGame.MODEL.Entities;
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

        public GameAppService(IMapper mapper,
                                IKingdomRepository kingdomRepository,
                                ICountyRepository countyRepository,
                                IUnitRepository unitRepository,
                                IAttackRepository attackRepository,
                                IUnitOfWork unitOfWork,
                                IGameRepository gameRepository,
                                IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.kingdomRepository = kingdomRepository;
            this.countyRepository = countyRepository;
            this.unitRepository = unitRepository;
            this.attackRepository = attackRepository;
            this.unitOfWork = unitOfWork;
            this.gameRepository = gameRepository;
            this.userRepository = userRepository;
        }

        public async Task<MainPageDto> GetCountyPage(Guid countyId)
        {
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

            return result;
        }

        public async Task<MainPageDto> GetMainPage(Guid kingdomId)
        {
            var kingdom = await kingdomRepository.GetKingdomAsync(kingdomId);
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

            return result;
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

                while(attackingArmy.Count != 0 || defendingArmy.Count != 0)
                {
                    var currentAttacker = attackingArmy[rnd.Next(attackingArmy.Count)];
                    var currentDefender = defendingArmy[rnd.Next(defendingArmy.Count)];
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
                if(attackingArmy.Count == 0)
                {
                    await kingdomRepository.TranferGold(attack.Attacker.KingdomId, attack.Defender.KingdomId, Convert.ToInt32(attackingKingdom.Gold * 0.1));
                    await countyRepository.TransferResourcesAsync(attack.AttackerCountyId, attack.DefenderCountyId, new ResourcesDto
                    {
                        Wood = Convert.ToInt32(attack.Attacker.Wood * 0.1),
                        Marble = Convert.ToInt32(attack.Attacker.Marble * 0.1),
                        Wine = Convert.ToInt32(attack.Attacker.Wine * 0.1),
                        Sulfur = Convert.ToInt32(attack.Attacker.Sulfur * 0.1)
                    });
                }
                else if (defendingArmy.Count == 0)
                {
                    await kingdomRepository.TranferGold(attack.Defender.KingdomId, attack.Attacker.KingdomId, Convert.ToInt32(attackingKingdom.Gold * 0.1));
                    await countyRepository.TransferResourcesAsync(attack.DefenderCountyId, attack.AttackerCountyId, new ResourcesDto
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
                foreach(var unit in attack.AttackerUnits.Units)
                {
                    await unitRepository.MoveToUnitGroup(unit.Id, attack.Attacker.Units.Id);
                }
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

                    foreach(var unit in county.Units.Units)
                    {
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
        }
    }
}
