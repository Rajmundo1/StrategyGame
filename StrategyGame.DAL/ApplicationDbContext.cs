using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StrategyGame.MODEL.Entities;
using StrategyGame.MODEL.Entities.Buildings;
using StrategyGame.MODEL.Entities.Technologies;
using StrategyGame.MODEL.Entities.Units;
using StrategyGame.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategyGame.DAL
{
    public class ApplicationDbContext: IdentityDbContext<User>
    {
        public override DbSet<User> Users { get; set; }
        public DbSet<Building> Buildings { get; set; } 
        public DbSet<Attack> Attacks { get; set; }
        public DbSet<County> Counties { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Kingdom> Kingdoms { get; set; }
        public DbSet<UnitLevel> UnitLevels { get; set; }
        public DbSet<BuildingLevel> BuildingLevels { get; set; }
        public DbSet<UnitGroup> UnitGroups { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<UnitSpecifics> UnitSpecifics { get; set; }
        public DbSet<TechnologySpecifics> TechnologySpecifics { get; set; }
        public DbSet<BuildingSpecifics> BuildingSpecifics { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            
            foreach(var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            #region Creating Entities
            var passwordHasher = new PasswordHasher<IdentityUser>();

            //TODO COST CHANGE
            var sawMillLevels = new List<BuildingLevel>
            {
                new BuildingLevel
                {
                    Id = Guid.Parse("c1221e4f-d781-405b-9944-9e18fc765fe8"),
                    BuildingSpecificsId = Guid.Parse("e2bfc4a7-d73f-4a2e-b91f-209c08a3f14f"),
                    Level = 1,
                    WoodProduction = 100,
                    WoodCost = 250
                },
                new BuildingLevel
                {
                    Id = Guid.Parse("73b692f5-591e-42a5-9f6f-d5b382cc5ccd"),
                    BuildingSpecificsId = Guid.Parse("e2bfc4a7-d73f-4a2e-b91f-209c08a3f14f"),
                    Level = 2,
                    WoodProduction = 500,
                    WoodCost = 15,//00,
                    MarbleCost = 2,//00
                },
                new BuildingLevel
                {
                    Id = Guid.Parse("770206ed-5f86-4ea9-a6cc-8b0a1083aa9b"),
                    BuildingSpecificsId = Guid.Parse("e2bfc4a7-d73f-4a2e-b91f-209c08a3f14f"),
                    Level = 3,
                    WoodProduction = 1000,
                    WoodCost = 10,//000,
                    MarbleCost = 15, //00,
                }
            };
            var quarryLevels = new List<BuildingLevel>
            {
                new BuildingLevel
                {
                    Id = Guid.Parse("16da0e13-b4dd-4221-a755-5746605f9331"),
                    BuildingSpecificsId = Guid.Parse("1d203260-0928-47b6-9d10-5e4cf0c70265"),
                    Level = 1,
                    MarbleProduction = 100,
                    WoodCost = 250
                },
                new BuildingLevel
                {
                    Id = Guid.Parse("e8268211-0c57-49c3-b88d-821876b372a5"),
                    BuildingSpecificsId = Guid.Parse("1d203260-0928-47b6-9d10-5e4cf0c70265"),
                    Level = 2,
                    MarbleProduction = 500,
                    WoodCost = 1500,
                    MarbleCost = 200
                },
                new BuildingLevel
                {
                    Id = Guid.Parse("b7bc3554-98d4-419c-a656-74b81cd33dfa"),
                    BuildingSpecificsId = Guid.Parse("1d203260-0928-47b6-9d10-5e4cf0c70265"),
                    Level = 3,
                    MarbleProduction = 1000,
                    WoodCost = 10000,
                    MarbleCost = 1500
                }
            };
            var wineryLevels = new List<BuildingLevel>
            {
                new BuildingLevel
                {
                    Id = Guid.Parse("f747608e-aafb-49c8-8912-e2e23ed98e3f"),
                    BuildingSpecificsId = Guid.Parse("4db1c8d2-b2b0-49a9-b8a5-8f9d5bbecddb"),
                    Level = 1,
                    WineProduction = 100,
                    WoodCost = 250
                },
                new BuildingLevel
                {
                    Id = Guid.Parse("a80feb0d-0e00-42ba-9b34-731376693a6b"),
                    BuildingSpecificsId = Guid.Parse("4db1c8d2-b2b0-49a9-b8a5-8f9d5bbecddb"),
                    Level = 2,
                    WineProduction = 500,
                    WoodCost = 1500,
                    MarbleCost = 200
                },
                new BuildingLevel
                {
                    Id = Guid.Parse("5d9746c5-bc3d-4164-850a-4778a6652c6e"),
                    BuildingSpecificsId = Guid.Parse("4db1c8d2-b2b0-49a9-b8a5-8f9d5bbecddb"),
                    Level = 3,
                    WineProduction = 1000,
                    WoodCost = 10000,
                    MarbleCost = 1500
                }
            };
            var sulfurMineLevels = new List<BuildingLevel>
            {
                new BuildingLevel
                {
                    Id = Guid.Parse("7906db1c-8193-4e27-aa30-8a30e5426eea"),
                    BuildingSpecificsId = Guid.Parse("d02e3c9c-f26c-4136-a904-27ad074fa456"),
                    Level = 1,
                    SulfurProduction = 100,
                    WoodCost = 250
                },
                new BuildingLevel
                {
                    Id = Guid.Parse("95805848-ee97-4496-8cb7-9cdaa55af49a"),
                    BuildingSpecificsId = Guid.Parse("d02e3c9c-f26c-4136-a904-27ad074fa456"),
                    Level = 2,
                    SulfurProduction = 500,
                    WoodCost = 1500,
                    MarbleCost = 200
                },
                new BuildingLevel
                {
                    Id = Guid.Parse("6bb9b1c6-b0ca-4a61-9b99-4efc86f92558"),
                    BuildingSpecificsId = Guid.Parse("d02e3c9c-f26c-4136-a904-27ad074fa456"),
                    Level = 3,
                    SulfurProduction = 1000,
                    WoodCost = 10000,
                    MarbleCost = 1500
                }
            };
            var academyLevels = new List<BuildingLevel>
            {
                new BuildingLevel
                {
                    Id = Guid.Parse("34cdbb37-be23-4867-b1f9-4e3bdcec10d9"),
                    BuildingSpecificsId = Guid.Parse("598fd678-5915-4c88-80d8-ff389c8278f9"),
                    Level = 1,
                    ResearchOutPut = 100,
                    WoodCost = 250,
                    WineCost = 100
                },
                new BuildingLevel
                {
                    Id = Guid.Parse("59cd37b0-dc91-432f-9e1f-cc54547eb807"),
                    BuildingSpecificsId = Guid.Parse("598fd678-5915-4c88-80d8-ff389c8278f9"),
                    Level = 2,
                    ResearchOutPut = 500,
                    WoodCost = 1500,
                    MarbleCost = 200,
                    WineCost = 800
                },
                new BuildingLevel
                {
                    Id = Guid.Parse("b0c9c409-5a66-4e7d-9eb5-17b5ce5ced17"),
                    BuildingSpecificsId = Guid.Parse("598fd678-5915-4c88-80d8-ff389c8278f9"),
                    Level = 3,
                    ResearchOutPut = 1000,
                    WoodCost = 10000,
                    MarbleCost = 1500,
                    WineCost = 2500
                }
            };
            var garrisonLevels = new List<BuildingLevel> 
            {
                new BuildingLevel
                {
                    Id = Guid.Parse("547ad002-b237-474e-b68c-0018ddf4b77a"),
                    BuildingSpecificsId = Guid.Parse("3a8ffb5d-6edb-4908-a72e-3d268128efee"),
                    Level = 1,
                    ForceLimitBonus = 100,
                    WoodCost = 250,
                    WineCost = 100
                },
                new BuildingLevel
                {
                    Id = Guid.Parse("205d6189-4fef-4777-afb8-9f73a835724e"),
                    BuildingSpecificsId = Guid.Parse("3a8ffb5d-6edb-4908-a72e-3d268128efee"),
                    Level = 2,
                    ForceLimitBonus = 500,
                    WoodCost = 1500,
                    MarbleCost = 200,
                    WineCost = 800
                },
                new BuildingLevel
                {
                    Id = Guid.Parse("00f807d6-9017-4f80-80ab-8544d9343f72"),
                    BuildingSpecificsId = Guid.Parse("3a8ffb5d-6edb-4908-a72e-3d268128efee"),
                    Level = 3,
                    ForceLimitBonus = 1000,
                    WoodCost = 10000,
                    MarbleCost = 1500,
                    WineCost = 2500,
                    SulfurCost = 2000
                }
            };

            var sawMillSpecifics = new BuildingSpecifics
            {
                Id = Guid.Parse("e2bfc4a7-d73f-4a2e-b91f-209c08a3f14f"),
                Description = "A sawmill that produces wood",
                Name = "Sawmill",
                ImageUrl = "/images/sawmill",
                MaxLevel = 3

            };
            var quarrySpecifics = new BuildingSpecifics
            {
                Id = Guid.Parse("1d203260-0928-47b6-9d10-5e4cf0c70265"),
                Description = "A marble quarry that produces marble",
                Name = "Quarry",
                ImageUrl = "/images/quarry",
                MaxLevel = 3

            };
            var winerySpecifics = new BuildingSpecifics
            {
                Id = Guid.Parse("4db1c8d2-b2b0-49a9-b8a5-8f9d5bbecddb"),
                Description = "A winery that produces wine",
                Name = "Winery",
                ImageUrl = "/images/winery",
                MaxLevel = 3

            };
            var sulfurMineSpecifics = new BuildingSpecifics
            {
                Id = Guid.Parse("d02e3c9c-f26c-4136-a904-27ad074fa456"),
                Description = "A sulfur mine that produces sulfur",
                Name = "Sulfur Mine",
                ImageUrl = "/images/sulfurMine",
                MaxLevel = 3
            };
            var academySpecifics = new BuildingSpecifics
            {
                Id = Guid.Parse("598fd678-5915-4c88-80d8-ff389c8278f9"),
                Description = "An academy that produces research points",
                Name = "Academy",
                ImageUrl = "/images/academy",
                MaxLevel = 3
            };
            var garrisonSpecifics = new BuildingSpecifics
            {
                Id = Guid.Parse("3a8ffb5d-6edb-4908-a72e-3d268128efee"),
                Description = "A garrison that accomodate units",
                Name = "Garrison",
                ImageUrl = "/images/garrison",
                MaxLevel = 3
            };

            var hopliteLevels = new List<UnitLevel>
            {
                new UnitLevel
                {
                    Id = Guid.Parse("3e1869eb-f12a-40e7-bec3-0b3ae410370a"),
                    ForceLimit = 1,
                    Level = 1,
                    UnitSpecificsId = Guid.Parse("97f6314a-766d-4aa2-9c49-7dbcf86140b5"),
                    AttackPower = 3,
                    DefensePower = 5,
                    WoodCost = 15,
                    GoldCost = 10,
                    GoldUpkeep = 5                    
                },
                new UnitLevel
                {
                    Id = Guid.Parse("26f967a9-44b9-4aa9-88ae-b3af7ac0ad09"),
                    ForceLimit = 2,
                    Level = 2,
                    UnitSpecificsId = Guid.Parse("97f6314a-766d-4aa2-9c49-7dbcf86140b5"),
                    AttackPower = 5,
                    DefensePower = 8,
                    WoodCost = 25,
                    MarbleCost = 5,
                    GoldCost = 15,
                    GoldUpkeep = 10
                },
                new UnitLevel
                {
                    Id = Guid.Parse("15ec08c3-a2e0-42a9-bd5a-37381e121dcb"),
                    ForceLimit = 3,
                    Level = 3,
                    UnitSpecificsId = Guid.Parse("97f6314a-766d-4aa2-9c49-7dbcf86140b5"),
                    AttackPower = 8,
                    DefensePower = 12,
                    WoodCost = 50,
                    MarbleCost = 50,
                    GoldCost = 30,
                    GoldUpkeep = 15
                }
            };
            var slingshotLevels = new List<UnitLevel>
            {
                new UnitLevel
                {
                    Id = Guid.Parse("2e560b5e-f626-4b25-8957-0337f385c2be"),
                    ForceLimit = 2,
                    Level = 1,
                    UnitSpecificsId = Guid.Parse("489e9070-f6f6-4130-8979-89e54b140835"),
                    AttackPower = 7,
                    DefensePower = 2,
                    WoodCost = 15,
                    MarbleCost = 10,
                    MarbleUpkeep = 5,
                    GoldCost = 10,
                    GoldUpkeep = 5
                },
                new UnitLevel
                {
                    Id = Guid.Parse("2c944d6f-a2ea-448a-a4f8-7756ef115263"),
                    ForceLimit = 3,
                    Level = 2,
                    UnitSpecificsId = Guid.Parse("489e9070-f6f6-4130-8979-89e54b140835"),
                    AttackPower = 10,
                    DefensePower = 4,
                    WoodCost = 30,
                    MarbleCost = 15,
                    MarbleUpkeep = 10,
                    GoldCost = 20,
                    GoldUpkeep = 10
                },
                new UnitLevel
                {
                    Id = Guid.Parse("b0563dec-ae9a-49df-bbc2-4db74bf5e2ec"),
                    ForceLimit = 4,
                    Level = 3,
                    UnitSpecificsId = Guid.Parse("489e9070-f6f6-4130-8979-89e54b140835"),
                    AttackPower = 15,
                    DefensePower = 6,
                    WoodCost = 50,
                    MarbleCost = 20,
                    MarbleUpkeep = 15,
                    GoldCost = 35,
                    GoldUpkeep = 20
                }
            };
            var catapultLevels = new List<UnitLevel>
            {
                new UnitLevel
                {
                    Id = Guid.Parse("e8cfbc91-d0c7-4ee4-a123-ce3e554026f8"),
                    ForceLimit = 5,
                    Level = 1,
                    UnitSpecificsId = Guid.Parse("06d69f35-d7f8-444f-bd25-da45bc6accb6"),
                    AttackPower = 10,
                    DefensePower = 10,
                    WoodCost = 100,
                    MarbleCost = 50,
                    SulfurCost = 50,
                    WoodUpkeep = 50,
                    MarbleUpkeep = 20,
                    GoldCost = 100,
                    GoldUpkeep = 20
                },
                new UnitLevel
                {
                    Id = Guid.Parse("cc847345-0747-45ee-ab9b-60a97873d9b1"),
                    ForceLimit = 10,
                    Level = 2,
                    UnitSpecificsId = Guid.Parse("06d69f35-d7f8-444f-bd25-da45bc6accb6"),
                    AttackPower = 20,
                    DefensePower = 20,
                    WoodCost = 200,
                    MarbleCost = 100,
                    SulfurCost = 100,
                    WoodUpkeep = 100,
                    MarbleUpkeep = 40,
                    GoldCost = 200,
                    GoldUpkeep = 50
                },
                new UnitLevel
                {
                    Id = Guid.Parse("9946dd5f-078a-4029-9ba1-0a3b22d8dfdc"),
                    ForceLimit = 15,
                    Level = 3,
                    UnitSpecificsId = Guid.Parse("06d69f35-d7f8-444f-bd25-da45bc6accb6"),
                    AttackPower = 30,
                    DefensePower = 30,
                    WoodCost = 400,
                    MarbleCost = 200,
                    SulfurCost = 200,
                    WoodUpkeep = 200,
                    MarbleUpkeep = 80,
                    GoldCost = 400,
                    GoldUpkeep = 100
                }
            };

            var hopliteSpecifics = new UnitSpecifics
            {
                Id = Guid.Parse("97f6314a-766d-4aa2-9c49-7dbcf86140b5"),
                Description = "A melee unit that is strong in close combat",
                ImageUrl = "/images/hoplite",
                MaxLevel = 3,
                Name = "Hoplite",            
            };
            var slingshotSpecifics = new UnitSpecifics
            {
                Id = Guid.Parse("489e9070-f6f6-4130-8979-89e54b140835"),
                Description = "A ranged unit that is strong in ranged combat",
                ImageUrl = "/images/slingshot",
                MaxLevel = 3,
                Name = "Slingshot",
            };
            var catapultSpecifics = new UnitSpecifics
            {
                Id = Guid.Parse("06d69f35-d7f8-444f-bd25-da45bc6accb6"),
                Description = "A ranged unit that is the strongest in ranged combat",
                ImageUrl = "/images/catapult",
                MaxLevel = 3,
                Name = "Catapult",
            };

            var productionBoosterResearch = new TechnologySpecifics
            {
                Id = Guid.Parse("a6336474-fa17-43ba-a5c6-7fee92ab15b7"),
                Description = "Boosts all production",
                Name = "Production Booster",
                PictureUrl = "/images/productionBooster",
                StoneBonus = 1.1,
                SulfurBonus = 1.1,
                WineBonus = 1.1,
                WoodBonus = 1.1,
                ResearchPointCost = 1000
            };
            var scienceBoosterResearch = new TechnologySpecifics
            {
                Id = Guid.Parse("f7f7f6a9-1ce5-4051-82b0-a55fb19d901c"),
                Description = "Boosts research output",
                Name = "Science Booster",
                PictureUrl = "/images/scienceBooster",
                ResearchBonus = 1.2,
                ResearchPointCost = 1000
            };
            var goldBoosterResearch = new TechnologySpecifics
            {
                Id = Guid.Parse("93ad7e45-7071-48d5-a5df-c5eb21bb35da"),
                Description = "Boosts gold production",
                Name = "Gold Production Booster",
                PictureUrl = "/images/goldProductionBooster",
                GoldBonus = 1.3,
                ResearchPointCost = 1000
            };
            var unitBoosterResearch = new TechnologySpecifics
            {
                Id = Guid.Parse("4e9f32b6-2621-4f7c-a939-f4d1a1a2daae"),
                Description = "Unit booster research",
                Name = "Unit Booster",
                PictureUrl = "/images/unitBooster",
                AttackPowerBonus = 1.15,
                DefensePowerBonus = 1.1,
                ResearchPointCost = 1000
            };

            var technology11 = new Technology
            {
                Id = Guid.Parse("0bfc558f-e32b-463e-9e74-06696bd7877a"),
                KingdomId = Guid.Parse("5fd3e0a3-0e0e-445a-93e6-8f94b6690794"),
                TechnologySpecificsId = Guid.Parse("a6336474-fa17-43ba-a5c6-7fee92ab15b7"),
                Status = ResearchStatus.Researched
            };
            var technology12 = new Technology
            {
                Id = Guid.Parse("fb6408b3-d806-43f1-9ee4-ffefc885e9ce"),
                KingdomId = Guid.Parse("5fd3e0a3-0e0e-445a-93e6-8f94b6690794"),
                TechnologySpecificsId = Guid.Parse("f7f7f6a9-1ce5-4051-82b0-a55fb19d901c"),
                Status = ResearchStatus.Researched
            };
            var technology13 = new Technology
            {
                Id = Guid.Parse("3444c5c0-db00-49fa-a0be-676e4d05864a"),
                KingdomId = Guid.Parse("5fd3e0a3-0e0e-445a-93e6-8f94b6690794"),
                TechnologySpecificsId = Guid.Parse("93ad7e45-7071-48d5-a5df-c5eb21bb35da"),
                Status = ResearchStatus.Researched
            };
            var technology14 = new Technology
            {
                Id = Guid.Parse("fce4e719-9a94-4a29-b9da-3ab5b87a910e"),
                KingdomId = Guid.Parse("5fd3e0a3-0e0e-445a-93e6-8f94b6690794"),
                TechnologySpecificsId = Guid.Parse("4e9f32b6-2621-4f7c-a939-f4d1a1a2daae"),
                Status = ResearchStatus.Researched
            };
            var technology21 = new Technology
            {
                Id = Guid.Parse("0d5fa209-cd89-4f26-bf6c-2f915a661226"),
                KingdomId = Guid.Parse("a37de913-486d-4df3-9025-1e5d4f881220"),
                TechnologySpecificsId = Guid.Parse("a6336474-fa17-43ba-a5c6-7fee92ab15b7"),
                Status = ResearchStatus.UnResearched
            };
            var technology22 = new Technology
            {
                Id = Guid.Parse("67648201-cb7a-4904-8622-b2863444eb82"),
                KingdomId = Guid.Parse("a37de913-486d-4df3-9025-1e5d4f881220"),
                TechnologySpecificsId = Guid.Parse("f7f7f6a9-1ce5-4051-82b0-a55fb19d901c"),
                Status = ResearchStatus.UnResearched
            };
            var technology23 = new Technology
            {
                Id = Guid.Parse("605b6d6a-40dd-415d-8726-46ef8c0592bf"),
                KingdomId = Guid.Parse("a37de913-486d-4df3-9025-1e5d4f881220"),
                TechnologySpecificsId = Guid.Parse("93ad7e45-7071-48d5-a5df-c5eb21bb35da"),
                Status = ResearchStatus.UnResearched
            };
            var technology24 = new Technology
            {
                Id = Guid.Parse("e12a9bfd-1847-4e43-ae91-bcce2c5545a3"),
                KingdomId = Guid.Parse("a37de913-486d-4df3-9025-1e5d4f881220"),
                TechnologySpecificsId = Guid.Parse("4e9f32b6-2621-4f7c-a939-f4d1a1a2daae"),
                Status = ResearchStatus.UnResearched
            };

            var buildings11 = new List<Building>
            {
                new Building
                {
                    Id = Guid.Parse("a3cf5dd1-478a-4ea2-a049-6bbb7f965863"),
                    BuildingSpecificsId = Guid.Parse("e2bfc4a7-d73f-4a2e-b91f-209c08a3f14f"),
                    CountyId = Guid.Parse("217f6d72-a33e-4612-b164-f1bbd5db94c2"),
                    Level = 1,
                    Status = BuildingStatus.Built
                },
                new Building
                {
                    Id = Guid.Parse("06ebbbb8-7154-4419-aea5-46d327594517"),
                    BuildingSpecificsId = Guid.Parse("1d203260-0928-47b6-9d10-5e4cf0c70265"),
                    CountyId = Guid.Parse("217f6d72-a33e-4612-b164-f1bbd5db94c2"),
                    Level = 1,
                    Status = BuildingStatus.Built
                },
                new Building
                {
                    Id = Guid.Parse("5b24eb70-9a6e-40de-b620-96f6dca50fab"),
                    BuildingSpecificsId = Guid.Parse("4db1c8d2-b2b0-49a9-b8a5-8f9d5bbecddb"),
                    CountyId = Guid.Parse("217f6d72-a33e-4612-b164-f1bbd5db94c2"),
                    Level = 1,
                    Status = BuildingStatus.Built
                },
                new Building
                {
                    Id = Guid.Parse("50c2da0b-43a4-42ae-97ab-c89009f7dd8d"),
                    BuildingSpecificsId = Guid.Parse("d02e3c9c-f26c-4136-a904-27ad074fa456"),
                    CountyId = Guid.Parse("217f6d72-a33e-4612-b164-f1bbd5db94c2"),
                    Level = 1,
                    Status = BuildingStatus.Built
                },
                new Building
                {
                    Id = Guid.Parse("219bd5e9-e20e-4393-adb3-b40d97eb276e"),
                    BuildingSpecificsId = Guid.Parse("598fd678-5915-4c88-80d8-ff389c8278f9"),
                    CountyId = Guid.Parse("217f6d72-a33e-4612-b164-f1bbd5db94c2"),
                    Level = 1,
                    Status = BuildingStatus.Built
                },
                new Building
                {
                    Id = Guid.Parse("042767db-2f76-4fdd-99b7-fa0b3054ba3a"),
                    BuildingSpecificsId = Guid.Parse("3a8ffb5d-6edb-4908-a72e-3d268128efee"),
                    CountyId = Guid.Parse("217f6d72-a33e-4612-b164-f1bbd5db94c2"),
                    Level = 3,
                    Status = BuildingStatus.Built
                },
            };
            var buildings12 = new List<Building>
            {
                new Building
                {
                    Id = Guid.Parse("326740fa-a052-4b34-9038-b825d9d3d5cb"),
                    BuildingSpecificsId = Guid.Parse("e2bfc4a7-d73f-4a2e-b91f-209c08a3f14f"),
                    CountyId = Guid.Parse("9160fe49-2966-4fb6-94d7-6999c7351368"),
                    Level = 1,
                    Status = BuildingStatus.NotBuilt
                },
                new Building
                {
                    Id = Guid.Parse("9dbb0a30-8b47-4843-9c03-06e7b4c501cf"),
                    BuildingSpecificsId = Guid.Parse("1d203260-0928-47b6-9d10-5e4cf0c70265"),
                    CountyId = Guid.Parse("9160fe49-2966-4fb6-94d7-6999c7351368"),
                    Level = 1,
                    Status = BuildingStatus.NotBuilt
                },
                new Building
                {
                    Id = Guid.Parse("ac2dbc91-0163-4064-a6a9-a13afaad7ced"),
                    BuildingSpecificsId = Guid.Parse("4db1c8d2-b2b0-49a9-b8a5-8f9d5bbecddb"),
                    CountyId = Guid.Parse("9160fe49-2966-4fb6-94d7-6999c7351368"),
                    Level = 1,
                    Status = BuildingStatus.NotBuilt
                },
                new Building
                {
                    Id = Guid.Parse("cff226fd-aa32-4942-a489-0d0dcb82cbae"),
                    BuildingSpecificsId = Guid.Parse("d02e3c9c-f26c-4136-a904-27ad074fa456"),
                    CountyId = Guid.Parse("9160fe49-2966-4fb6-94d7-6999c7351368"),
                    Level = 1,
                    Status = BuildingStatus.NotBuilt
                },
                new Building
                {
                    Id = Guid.Parse("1bda65c2-81c2-40cd-abd4-a022ef6ae658"),
                    BuildingSpecificsId = Guid.Parse("598fd678-5915-4c88-80d8-ff389c8278f9"),
                    CountyId = Guid.Parse("9160fe49-2966-4fb6-94d7-6999c7351368"),
                    Level = 1,
                    Status = BuildingStatus.NotBuilt
                },
                new Building
                {
                    Id = Guid.Parse("76698e37-4e57-4405-895d-39812a0000b9"),
                    BuildingSpecificsId = Guid.Parse("3a8ffb5d-6edb-4908-a72e-3d268128efee"),
                    CountyId = Guid.Parse("9160fe49-2966-4fb6-94d7-6999c7351368"),
                    Level = 1,
                    Status = BuildingStatus.Built
                }
            };
            var buildings21 = new List<Building>
            {
                new Building
                {
                    Id = Guid.Parse("fbcc61c5-8886-4703-ae57-8406be7c0713"),
                    BuildingSpecificsId = Guid.Parse("e2bfc4a7-d73f-4a2e-b91f-209c08a3f14f"),
                    CountyId = Guid.Parse("610fb8b0-386e-4b0d-9a51-59403fd686b6"),
                    Level = 1,
                    Status = BuildingStatus.Built
                },
                new Building
                {
                    Id = Guid.Parse("e52bd2ac-c58e-4277-a622-545c257fd1d9"),
                    BuildingSpecificsId = Guid.Parse("1d203260-0928-47b6-9d10-5e4cf0c70265"),
                    CountyId = Guid.Parse("610fb8b0-386e-4b0d-9a51-59403fd686b6"),
                    Level = 1,
                    Status = BuildingStatus.Built
                },
                new Building
                {
                    Id = Guid.Parse("61156095-4f2f-4d44-8fc4-e15b278557a2"),
                    BuildingSpecificsId = Guid.Parse("4db1c8d2-b2b0-49a9-b8a5-8f9d5bbecddb"),
                    CountyId = Guid.Parse("610fb8b0-386e-4b0d-9a51-59403fd686b6"),
                    Level = 1,
                    Status = BuildingStatus.Built
                },
                new Building
                {
                    Id = Guid.Parse("cd118d28-dc24-44ca-97c7-af2c6f53aa58"),
                    BuildingSpecificsId = Guid.Parse("d02e3c9c-f26c-4136-a904-27ad074fa456"),
                    CountyId = Guid.Parse("610fb8b0-386e-4b0d-9a51-59403fd686b6"),
                    Level = 1,
                    Status = BuildingStatus.Built
                },
                new Building
                {
                    Id = Guid.Parse("37819138-f604-42bb-9b7d-90f954da0ce2"),
                    BuildingSpecificsId = Guid.Parse("598fd678-5915-4c88-80d8-ff389c8278f9"),
                    CountyId = Guid.Parse("610fb8b0-386e-4b0d-9a51-59403fd686b6"),
                    Level = 1,
                    Status = BuildingStatus.Built
                },
                new Building
                {
                    Id = Guid.Parse("b1a4a941-00d1-4655-b96f-3fcd6ccff69c"),
                    BuildingSpecificsId = Guid.Parse("3a8ffb5d-6edb-4908-a72e-3d268128efee"),
                    CountyId = Guid.Parse("610fb8b0-386e-4b0d-9a51-59403fd686b6"),
                    Level = 1,
                    Status = BuildingStatus.Built
                }
            };
            var buildings22 = new List<Building>
            {
                new Building
                {
                    Id = Guid.Parse("8a6390d8-abbe-4f41-ba33-ffbae69bb183"),
                    BuildingSpecificsId = Guid.Parse("e2bfc4a7-d73f-4a2e-b91f-209c08a3f14f"),
                    CountyId = Guid.Parse("01ef4de3-61c4-4671-bcd3-4b5009dea2d2"),
                    Level = 1,
                    Status = BuildingStatus.NotBuilt
                },
                new Building
                {
                    Id = Guid.Parse("9be0efdd-f662-4c7e-8ae3-6cb576f3ea9e"),
                    BuildingSpecificsId = Guid.Parse("1d203260-0928-47b6-9d10-5e4cf0c70265"),
                    CountyId = Guid.Parse("01ef4de3-61c4-4671-bcd3-4b5009dea2d2"),
                    Level = 1,
                    Status = BuildingStatus.NotBuilt
                },
                new Building
                {
                    Id = Guid.Parse("2f7b1592-f75f-461b-a497-6344b64737a4"),
                    BuildingSpecificsId = Guid.Parse("4db1c8d2-b2b0-49a9-b8a5-8f9d5bbecddb"),
                    CountyId = Guid.Parse("01ef4de3-61c4-4671-bcd3-4b5009dea2d2"),
                    Level = 1,
                    Status = BuildingStatus.NotBuilt
                },
                new Building
                {
                    Id = Guid.Parse("19d2606c-9e43-45a8-8631-2dfa58361822"),
                    BuildingSpecificsId = Guid.Parse("d02e3c9c-f26c-4136-a904-27ad074fa456"),
                    CountyId = Guid.Parse("01ef4de3-61c4-4671-bcd3-4b5009dea2d2"),
                    Level = 1,
                    Status = BuildingStatus.NotBuilt
                },
                new Building
                {
                    Id = Guid.Parse("bab4441b-58f3-450d-869b-17a89b990ffa"),
                    BuildingSpecificsId = Guid.Parse("598fd678-5915-4c88-80d8-ff389c8278f9"),
                    CountyId = Guid.Parse("01ef4de3-61c4-4671-bcd3-4b5009dea2d2"),
                    Level = 1,
                    Status = BuildingStatus.NotBuilt
                },
                new Building
                {
                    Id = Guid.Parse("0a4b02fa-ea0a-469a-8695-7c3ff01534dd"),
                    BuildingSpecificsId = Guid.Parse("3a8ffb5d-6edb-4908-a72e-3d268128efee"),
                    CountyId = Guid.Parse("01ef4de3-61c4-4671-bcd3-4b5009dea2d2"),
                    Level = 1,
                    Status = BuildingStatus.Built
                }
            };

            var unitList11 = new List<Unit>
            {
                new Unit
                {
                    Id = Guid.Parse("9d7f181c-8933-483d-af15-d50a0a599c87"),
                    Level = 1,
                    UnitGroupId = Guid.Parse("05926428-3186-4730-85c6-31b740cd9e5d"),
                    UnitSpecificsId = Guid.Parse("97f6314a-766d-4aa2-9c49-7dbcf86140b5")
                },
                new Unit
                {
                    Id = Guid.Parse("6edbded8-6fe4-4f84-b7ac-1a4ea8edce6b"),
                    Level = 3,
                    UnitGroupId = Guid.Parse("05926428-3186-4730-85c6-31b740cd9e5d"),
                    UnitSpecificsId = Guid.Parse("97f6314a-766d-4aa2-9c49-7dbcf86140b5")
                },
                new Unit
                {
                    Id = Guid.Parse("3454f1bd-a857-401d-9da7-c51ba635f198"),
                    Level = 1,
                    UnitGroupId = Guid.Parse("05926428-3186-4730-85c6-31b740cd9e5d"),
                    UnitSpecificsId = Guid.Parse("489e9070-f6f6-4130-8979-89e54b140835"),
                },
                new Unit
                {
                    Id = Guid.Parse("2229d5a6-09cb-4e40-93b9-3a2367d3b645"),
                    Level = 2,
                    UnitGroupId = Guid.Parse("05926428-3186-4730-85c6-31b740cd9e5d"),
                    UnitSpecificsId = Guid.Parse("06d69f35-d7f8-444f-bd25-da45bc6accb6"),
                }
            };
            var unitList21 = new List<Unit>
            {
                new Unit
                {
                    Id = Guid.Parse("5ff8abb8-6cf9-4c16-84fb-b8bd4f6e6741"),
                    Level = 1,
                    UnitGroupId = Guid.Parse("1a4b7681-b373-420d-aaa0-c5ec80e00b16"),
                    UnitSpecificsId = Guid.Parse("97f6314a-766d-4aa2-9c49-7dbcf86140b5")
                },
                new Unit
                {
                    Id = Guid.Parse("abd37410-d250-4e8c-8e5c-256ae2836f36"),
                    Level = 2,
                    UnitGroupId = Guid.Parse("1a4b7681-b373-420d-aaa0-c5ec80e00b16"),
                    UnitSpecificsId = Guid.Parse("489e9070-f6f6-4130-8979-89e54b140835"),
                },
                new Unit
                {
                    Id = Guid.Parse("7a904252-9830-45f6-949d-b742c210975b"),
                    Level = 2,
                    UnitGroupId = Guid.Parse("1a4b7681-b373-420d-aaa0-c5ec80e00b16"),
                    UnitSpecificsId = Guid.Parse("489e9070-f6f6-4130-8979-89e54b140835"),
                },
                new Unit
                {
                    Id = Guid.Parse("fa09b553-57f6-4f3a-ac29-acaec501d22c"),
                    Level = 3,
                    UnitGroupId = Guid.Parse("1a4b7681-b373-420d-aaa0-c5ec80e00b16"),
                    UnitSpecificsId = Guid.Parse("06d69f35-d7f8-444f-bd25-da45bc6accb6"),
                }
            };

            var unitGroup11 = new UnitGroup
            {
                Id = Guid.Parse("05926428-3186-4730-85c6-31b740cd9e5d"),
                AttackId = null,
                CountyId = Guid.Parse("217f6d72-a33e-4612-b164-f1bbd5db94c2"),
            };
            var unitGroup12 = new UnitGroup
            {
                Id = Guid.Parse("739dbea4-c91c-4ba2-9ea5-62115d317e46"),
                AttackId = null,
                CountyId = Guid.Parse("9160fe49-2966-4fb6-94d7-6999c7351368"),
            };
            var unitGroup21 = new UnitGroup
            {
                Id = Guid.Parse("1a4b7681-b373-420d-aaa0-c5ec80e00b16"),
                AttackId = null,
                CountyId = Guid.Parse("610fb8b0-386e-4b0d-9a51-59403fd686b6"),
            };
            var unitGroup22 = new UnitGroup
            {
                Id = Guid.Parse("b32e83be-34f0-4bed-b225-966887eb13e4"),
                AttackId = null,
                CountyId = Guid.Parse("01ef4de3-61c4-4671-bcd3-4b5009dea2d2"),
            };

            var county11 = new County
            {
                Id = Guid.Parse("217f6d72-a33e-4612-b164-f1bbd5db94c2"),
                KingdomId = Guid.Parse("5fd3e0a3-0e0e-445a-93e6-8f94b6690794"),
                Name = "Kingdom1 County1",
                TaxRate = 1.0,
                WineConsumption = 0,
                Wood = 5000,
                Marble = 5000,
                Wine = 2000,
                Sulfur = 1000,
                BasePopulation = 200,
                //UnitGroupId = Guid.Parse("05926428-3186-4730-85c6-31b740cd9e5d"),
            };
            var county12 = new County
            {
                Id = Guid.Parse("9160fe49-2966-4fb6-94d7-6999c7351368"),
                KingdomId = Guid.Parse("5fd3e0a3-0e0e-445a-93e6-8f94b6690794"),
                Name = "Kingdom1 County2",
                TaxRate = 1.0,
                WineConsumption = 0,
                Wood = 5000,
                Marble = 5000,
                Wine = 2000,
                Sulfur = 1000,
                BasePopulation = 200,
            };
            var county21 = new County
            {
                Id = Guid.Parse("610fb8b0-386e-4b0d-9a51-59403fd686b6"),
                KingdomId = Guid.Parse("a37de913-486d-4df3-9025-1e5d4f881220"),
                Name = "Kingdom2 County1",
                TaxRate = 1.0,
                WineConsumption = 0,
                Wood = 5000,
                Marble = 5000,
                Wine = 2000,
                Sulfur = 1000,
                BasePopulation = 200,
                //UnitGroupId = Guid.Parse("1a4b7681-b373-420d-aaa0-c5ec80e00b16"),
            };
            var county22 = new County
            {
                Id = Guid.Parse("01ef4de3-61c4-4671-bcd3-4b5009dea2d2"),
                KingdomId = Guid.Parse("a37de913-486d-4df3-9025-1e5d4f881220"),
                Name = "Kingdom2 County2",
                TaxRate = 1.0,
                WineConsumption = 0,
                Wood = 5000,
                Marble = 5000,
                Wine = 2000,
                Sulfur = 1000,
                BasePopulation = 200,
            };

            var counties1 = new List<County>{
                county11,
                county12
            };
            var counties2 = new List<County>{
                county21,
                county22
            };

            var kingdom1 = new Kingdom
            {
                Id = Guid.Parse("5fd3e0a3-0e0e-445a-93e6-8f94b6690794"),
                Gold = 3000,
                ResearchPoint = 2000,
            };
            var kingdom2 = new Kingdom
            {
                Id = Guid.Parse("a37de913-486d-4df3-9025-1e5d4f881220"),
                Gold = 3000,
                ResearchPoint = 2000,
            };

            var user1 = new User
            {
                Id = "ff5e4b7f-c83d-4070-a91a-a33de1b19405",
                UserName = "Rajmundo1",
                NormalizedUserName = "RAJMUNDO1",
                GameId = Guid.Parse("1bb1f3c1-8c10-439c-8dcb-7f8cc1f8044e"),
                KingdomId = Guid.Parse("5fd3e0a3-0e0e-445a-93e6-8f94b6690794"),
                ScoreboardPlace = 1
            };
            user1.PasswordHash = passwordHasher.HashPassword(user1, "Password1");
            var user2 = new User
            {
                Id = "b63d4aee-70d2-4d84-93a6-56c9db32aa11",
                UserName = "TestUser",
                NormalizedUserName = "TESTUSER",
                GameId = Guid.Parse("1bb1f3c1-8c10-439c-8dcb-7f8cc1f8044e"),
                KingdomId = Guid.Parse("a37de913-486d-4df3-9025-1e5d4f881220"),
                ScoreboardPlace = 2,

            };
            user2.PasswordHash = passwordHasher.HashPassword(user2, "Password1");

            var game = new Game
            {
                Id = Guid.Parse("1bb1f3c1-8c10-439c-8dcb-7f8cc1f8044e"),
                Round = 0,
            };
            #endregion

            #region Data Seed
            builder.Entity<Game>()
                .HasData(game);

            builder.Entity<User>()
                .HasData(new User[]{
                    user1,
                    user2
                });

            builder.Entity<BuildingLevel>()
                .HasData(new BuildingLevel[]{
                    sawMillLevels[0],
                    sawMillLevels[1],
                    sawMillLevels[2],
                    quarryLevels[0],
                    quarryLevels[1],
                    quarryLevels[2],
                    wineryLevels[0],
                    wineryLevels[1],
                    wineryLevels[2],
                    sulfurMineLevels[0],
                    sulfurMineLevels[1],
                    sulfurMineLevels[2],
                    academyLevels[0],
                    academyLevels[1],
                    academyLevels[2],
                    garrisonLevels[0],
                    garrisonLevels[1],
                    garrisonLevels[2]
                });

            builder.Entity<BuildingSpecifics>()
                .HasData(new BuildingSpecifics[]
                {
                    sawMillSpecifics,
                    quarrySpecifics,
                    winerySpecifics,
                    sulfurMineSpecifics,
                    academySpecifics,
                    garrisonSpecifics
                });

            builder.Entity<TechnologySpecifics>()
                .HasData(new TechnologySpecifics[]
                {
                    productionBoosterResearch,
                    scienceBoosterResearch,
                    goldBoosterResearch,
                    unitBoosterResearch
                });

            builder.Entity<UnitSpecifics>()
                .HasData(new UnitSpecifics[]
                {
                    hopliteSpecifics,
                    slingshotSpecifics,
                    catapultSpecifics
                });

            builder.Entity<UnitGroup>()
                .HasData(new UnitGroup[]
                {
                    unitGroup11,
                    unitGroup12,
                    unitGroup21,
                    unitGroup22                    
                });

            builder.Entity<UnitLevel>()
                .HasData(new UnitLevel[]
                {
                    hopliteLevels[0],
                    hopliteLevels[1],
                    hopliteLevels[2],
                    slingshotLevels[0],
                    slingshotLevels[1],
                    slingshotLevels[2],
                    catapultLevels[0],
                    catapultLevels[1],
                    catapultLevels[2]
                });

            builder.Entity<Kingdom>()
                .HasData(new Kingdom[]
                {
                    kingdom1,
                    kingdom2
                });

            builder.Entity<Technology>()
                .HasData(new Technology[]
                {
                    technology11,
                    technology12,
                    technology13,
                    technology14,
                    technology21,
                    technology22,
                    technology23,
                    technology24
                });

            builder.Entity<Unit>()
                .HasData(new Unit[]{
                    unitList11[0],
                    unitList11[1],
                    unitList11[2],
                    unitList11[3],
                    unitList21[0],
                    unitList21[1],
                    unitList21[2],
                    unitList21[3],
                });

            builder.Entity<County>()
                .HasData(new County[]
                {
                    counties1[0],
                    counties1[1],
                    counties2[0],
                    counties2[1]
                });

            builder.Entity<Building>()
                .HasData(new Building[]{
                    buildings11[0],
                    buildings11[1],
                    buildings11[2],
                    buildings11[3],
                    buildings11[4],
                    buildings11[5],
                    buildings12[0],
                    buildings12[1],
                    buildings12[2],
                    buildings12[3],
                    buildings12[4],
                    buildings12[5],
                    buildings21[0],
                    buildings21[1],
                    buildings21[2],
                    buildings21[3],
                    buildings21[4],
                    buildings21[5],
                    buildings22[0],
                    buildings22[1],
                    buildings22[2],
                    buildings22[3],
                    buildings22[4],
                    buildings22[5]
                });
#endregion
        }
    }
}
