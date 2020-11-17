using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StrategyGame.DAL.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuildingSpecifics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    MaxLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingSpecifics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Round = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Game", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kingdoms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Gold = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kingdoms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechnologySpecifics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PictureUrl = table.Column<string>(nullable: true),
                    WoodBonus = table.Column<double>(nullable: false),
                    StoneBonus = table.Column<double>(nullable: false),
                    WineBonus = table.Column<double>(nullable: false),
                    SulfurBonus = table.Column<double>(nullable: false),
                    GoldBonus = table.Column<double>(nullable: false),
                    ResearchBonus = table.Column<double>(nullable: false),
                    AttackPowerBonus = table.Column<double>(nullable: false),
                    DefensePowerBonus = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnologySpecifics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnitSpecifics",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    MaxLevel = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitSpecifics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BuildingLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BuildingSpecificsId = table.Column<Guid>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    PopulationBonus = table.Column<int>(nullable: false),
                    ForceLimitBonus = table.Column<int>(nullable: false),
                    WoodProduction = table.Column<int>(nullable: false),
                    MarbleProduction = table.Column<int>(nullable: false),
                    WineProduction = table.Column<int>(nullable: false),
                    SulfurProduction = table.Column<int>(nullable: false),
                    ResearchOutPut = table.Column<int>(nullable: false),
                    WoodCost = table.Column<int>(nullable: false),
                    MarbleCost = table.Column<int>(nullable: false),
                    WineCost = table.Column<int>(nullable: false),
                    SulfurCost = table.Column<int>(nullable: false),
                    GoldCost = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildingLevels_BuildingSpecifics_BuildingSpecificsId",
                        column: x => x.BuildingSpecificsId,
                        principalTable: "BuildingSpecifics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ScoreboardPlace = table.Column<int>(nullable: false),
                    KingdomId = table.Column<Guid>(nullable: true),
                    GameId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Kingdoms_KingdomId",
                        column: x => x.KingdomId,
                        principalTable: "Kingdoms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Counties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    KingdomId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TaxRate = table.Column<double>(nullable: false),
                    WineConsumption = table.Column<int>(nullable: false),
                    Wood = table.Column<int>(nullable: false),
                    Marble = table.Column<int>(nullable: false),
                    Wine = table.Column<int>(nullable: false),
                    Sulfur = table.Column<int>(nullable: false),
                    taxPerPop = table.Column<int>(nullable: false),
                    ResearchRoundLeft = table.Column<int>(nullable: false),
                    BuildingRoundLeft = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Counties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Counties_Kingdoms_KingdomId",
                        column: x => x.KingdomId,
                        principalTable: "Kingdoms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    KingdomId = table.Column<Guid>(nullable: false),
                    TechnologySpecificsId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Technologies_Kingdoms_KingdomId",
                        column: x => x.KingdomId,
                        principalTable: "Kingdoms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Technologies_TechnologySpecifics_TechnologySpecificsId",
                        column: x => x.TechnologySpecificsId,
                        principalTable: "TechnologySpecifics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnitLevels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UnitSpecificsId = table.Column<Guid>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    RangedAttackPower = table.Column<int>(nullable: false),
                    RangedDefensePower = table.Column<int>(nullable: false),
                    MeleeAttackPower = table.Column<int>(nullable: false),
                    MeleeDefensePower = table.Column<int>(nullable: false),
                    ForceLimit = table.Column<int>(nullable: false),
                    WoodCost = table.Column<int>(nullable: false),
                    MarbleCost = table.Column<int>(nullable: false),
                    WineCost = table.Column<int>(nullable: false),
                    SulfurCost = table.Column<int>(nullable: false),
                    GoldCost = table.Column<int>(nullable: false),
                    WoodUpkeep = table.Column<int>(nullable: false),
                    MarbleUpkeep = table.Column<int>(nullable: false),
                    WineUpkeep = table.Column<int>(nullable: false),
                    SulfurUpkeep = table.Column<int>(nullable: false),
                    GoldUpkeep = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitLevels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitLevels_UnitSpecifics_UnitSpecificsId",
                        column: x => x.UnitSpecificsId,
                        principalTable: "UnitSpecifics",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AttackerCountyId = table.Column<Guid>(nullable: false),
                    AttackerId = table.Column<Guid>(nullable: true),
                    DefenderCountyId = table.Column<Guid>(nullable: false),
                    DefenderId = table.Column<Guid>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attacks_Counties_AttackerId",
                        column: x => x.AttackerId,
                        principalTable: "Counties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Attacks_Counties_DefenderId",
                        column: x => x.DefenderId,
                        principalTable: "Counties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Buildings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CountyId = table.Column<Guid>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    BuildingSpecificsId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buildings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Buildings_BuildingSpecifics_BuildingSpecificsId",
                        column: x => x.BuildingSpecificsId,
                        principalTable: "BuildingSpecifics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Buildings_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnitGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AttackId = table.Column<Guid>(nullable: true),
                    CountyId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitGroups_Attacks_AttackId",
                        column: x => x.AttackId,
                        principalTable: "Attacks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnitGroups_Counties_CountyId",
                        column: x => x.CountyId,
                        principalTable: "Counties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UnitGroupId = table.Column<Guid>(nullable: false),
                    UnitSpecificsId = table.Column<Guid>(nullable: false),
                    Level = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_UnitGroups_UnitGroupId",
                        column: x => x.UnitGroupId,
                        principalTable: "UnitGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Units_UnitSpecifics_UnitSpecificsId",
                        column: x => x.UnitSpecificsId,
                        principalTable: "UnitSpecifics",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "BuildingSpecifics",
                columns: new[] { "Id", "Description", "ImageUrl", "MaxLevel", "Name" },
                values: new object[,]
                {
                    { new Guid("e2bfc4a7-d73f-4a2e-b91f-209c08a3f14f"), "A sawmill that produces wood", "/images/sawmill", 3, "Sawmill" },
                    { new Guid("1d203260-0928-47b6-9d10-5e4cf0c70265"), "A marble quarry that produces marble", "/images/quarry", 3, "Quarry" },
                    { new Guid("4db1c8d2-b2b0-49a9-b8a5-8f9d5bbecddb"), "A winery that produces wine", "/images/winery", 3, "Winery" },
                    { new Guid("d02e3c9c-f26c-4136-a904-27ad074fa456"), "A sulfur mine that produces sulfur", "/images/sulfur", 3, "Sulfur" }
                });

            migrationBuilder.InsertData(
                table: "Game",
                columns: new[] { "Id", "Round" },
                values: new object[] { new Guid("1bb1f3c1-8c10-439c-8dcb-7f8cc1f8044e"), 0 });

            migrationBuilder.InsertData(
                table: "Kingdoms",
                columns: new[] { "Id", "Gold", "UserId" },
                values: new object[,]
                {
                    { new Guid("5fd3e0a3-0e0e-445a-93e6-8f94b6690794"), 3000, new Guid("ff5e4b7f-c83d-4070-a91a-a33de1b19405") },
                    { new Guid("a37de913-486d-4df3-9025-1e5d4f881220"), 3000, new Guid("b63d4aee-70d2-4d84-93a6-56c9db32aa11") }
                });

            migrationBuilder.InsertData(
                table: "TechnologySpecifics",
                columns: new[] { "Id", "AttackPowerBonus", "DefensePowerBonus", "Description", "GoldBonus", "Name", "PictureUrl", "ResearchBonus", "StoneBonus", "SulfurBonus", "WineBonus", "WoodBonus" },
                values: new object[,]
                {
                    { new Guid("a6336474-fa17-43ba-a5c6-7fee92ab15b7"), 0.0, 0.0, "Boosts all production", 0.0, "Production Booster", "/images/productionBooster", 0.0, 1.1000000000000001, 1.1000000000000001, 1.1000000000000001, 1.1000000000000001 },
                    { new Guid("f7f7f6a9-1ce5-4051-82b0-a55fb19d901c"), 0.0, 0.0, "Boosts research output", 0.0, "Science Booster", "/images/scienceBooster", 1.2, 0.0, 0.0, 0.0, 0.0 },
                    { new Guid("93ad7e45-7071-48d5-a5df-c5eb21bb35da"), 0.0, 0.0, "Boosts gold production", 1.3, "Gold Production Booster", "/images/goldProductionBooster", 0.0, 0.0, 0.0, 0.0, 0.0 },
                    { new Guid("4e9f32b6-2621-4f7c-a939-f4d1a1a2daae"), 1.1499999999999999, 1.1000000000000001, "Unit booster research", 0.0, "Unit Booster", "/images/unitBooster", 0.0, 0.0, 0.0, 0.0, 0.0 }
                });

            migrationBuilder.InsertData(
                table: "UnitSpecifics",
                columns: new[] { "Id", "Description", "ImageUrl", "MaxLevel", "Name" },
                values: new object[,]
                {
                    { new Guid("97f6314a-766d-4aa2-9c49-7dbcf86140b5"), "A melee unit that is strong in close combat", "/images/hoplite", 3, "Hoplite" },
                    { new Guid("489e9070-f6f6-4130-8979-89e54b140835"), "A ranged unit that is strong in ranged combat", "/images/slingshot", 3, "Slingshot" },
                    { new Guid("06d69f35-d7f8-444f-bd25-da45bc6accb6"), "A ranged unit that is the strongest in ranged combat", "/images/catapult", 3, "Catapult" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "GameId", "KingdomId", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ScoreboardPlace", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "ff5e4b7f-c83d-4070-a91a-a33de1b19405", 0, "3adaee4d-cc31-46e6-9e4c-c09aa9a625a3", null, false, new Guid("1bb1f3c1-8c10-439c-8dcb-7f8cc1f8044e"), null, false, null, null, null, "AQAAAAEAACcQAAAAEEHF15ubqbIJKD6L3uYgaWRiFjUcs562pwdRB6CPGiAEz2AOieTZhatQnhIdGJCL/w==", null, false, 1, "ae20c1cd-284f-4bdf-aab0-170fbff5adcf", false, "Rajmundo1" },
                    { "b63d4aee-70d2-4d84-93a6-56c9db32aa11", 0, "59797d43-d959-452c-a5b2-314285cdb2e8", null, false, new Guid("1bb1f3c1-8c10-439c-8dcb-7f8cc1f8044e"), null, false, null, null, null, "AQAAAAEAACcQAAAAEABl9pjtf/nZNyb8BQywSXSlnRJl/NqMn85/eDIO15H4LT12xLOVMUD8Kkd+O0QV6A==", null, false, 2, "f47914dd-3137-473a-86bf-4567e2063217", false, "TestUser" }
                });

            migrationBuilder.InsertData(
                table: "BuildingLevels",
                columns: new[] { "Id", "BuildingSpecificsId", "ForceLimitBonus", "GoldCost", "Level", "MarbleCost", "MarbleProduction", "PopulationBonus", "ResearchOutPut", "SulfurCost", "SulfurProduction", "WineCost", "WineProduction", "WoodCost", "WoodProduction" },
                values: new object[,]
                {
                    { new Guid("c1221e4f-d781-405b-9944-9e18fc765fe8"), new Guid("e2bfc4a7-d73f-4a2e-b91f-209c08a3f14f"), 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 250, 100 },
                    { new Guid("73b692f5-591e-42a5-9f6f-d5b382cc5ccd"), new Guid("e2bfc4a7-d73f-4a2e-b91f-209c08a3f14f"), 0, 0, 2, 200, 0, 0, 0, 0, 0, 0, 0, 1500, 500 },
                    { new Guid("770206ed-5f86-4ea9-a6cc-8b0a1083aa9b"), new Guid("e2bfc4a7-d73f-4a2e-b91f-209c08a3f14f"), 0, 0, 3, 1500, 0, 0, 0, 0, 0, 0, 0, 10000, 1000 },
                    { new Guid("16da0e13-b4dd-4221-a755-5746605f9331"), new Guid("1d203260-0928-47b6-9d10-5e4cf0c70265"), 0, 0, 1, 0, 100, 0, 0, 0, 0, 0, 0, 250, 0 },
                    { new Guid("e8268211-0c57-49c3-b88d-821876b372a5"), new Guid("1d203260-0928-47b6-9d10-5e4cf0c70265"), 0, 0, 2, 200, 500, 0, 0, 0, 0, 0, 0, 1500, 0 },
                    { new Guid("b7bc3554-98d4-419c-a656-74b81cd33dfa"), new Guid("1d203260-0928-47b6-9d10-5e4cf0c70265"), 0, 0, 3, 1500, 1000, 0, 0, 0, 0, 0, 0, 10000, 0 },
                    { new Guid("f747608e-aafb-49c8-8912-e2e23ed98e3f"), new Guid("4db1c8d2-b2b0-49a9-b8a5-8f9d5bbecddb"), 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 100, 250, 0 },
                    { new Guid("a80feb0d-0e00-42ba-9b34-731376693a6b"), new Guid("4db1c8d2-b2b0-49a9-b8a5-8f9d5bbecddb"), 0, 0, 2, 200, 0, 0, 0, 0, 0, 0, 500, 1500, 0 },
                    { new Guid("5d9746c5-bc3d-4164-850a-4778a6652c6e"), new Guid("4db1c8d2-b2b0-49a9-b8a5-8f9d5bbecddb"), 0, 0, 3, 1500, 0, 0, 0, 0, 0, 0, 1000, 10000, 0 },
                    { new Guid("7906db1c-8193-4e27-aa30-8a30e5426eea"), new Guid("d02e3c9c-f26c-4136-a904-27ad074fa456"), 0, 0, 1, 0, 0, 0, 0, 0, 100, 0, 0, 250, 0 },
                    { new Guid("95805848-ee97-4496-8cb7-9cdaa55af49a"), new Guid("d02e3c9c-f26c-4136-a904-27ad074fa456"), 0, 0, 2, 200, 0, 0, 0, 0, 500, 0, 0, 1500, 0 },
                    { new Guid("6bb9b1c6-b0ca-4a61-9b99-4efc86f92558"), new Guid("d02e3c9c-f26c-4136-a904-27ad074fa456"), 0, 0, 3, 1500, 0, 0, 0, 0, 1000, 0, 0, 10000, 0 }
                });

            migrationBuilder.InsertData(
                table: "Counties",
                columns: new[] { "Id", "BuildingRoundLeft", "KingdomId", "Marble", "Name", "ResearchRoundLeft", "Sulfur", "TaxRate", "Wine", "WineConsumption", "Wood", "taxPerPop" },
                values: new object[,]
                {
                    { new Guid("610fb8b0-386e-4b0d-9a51-59403fd686b6"), 0, new Guid("5fd3e0a3-0e0e-445a-93e6-8f94b6690794"), 5000, "Kingdom2 County1", 0, 1000, 1.0, 2000, 0, 5000, 5 },
                    { new Guid("01ef4de3-61c4-4671-bcd3-4b5009dea2d2"), 0, new Guid("5fd3e0a3-0e0e-445a-93e6-8f94b6690794"), 5000, "Kingdom2 County2", 0, 1000, 1.0, 2000, 0, 5000, 5 },
                    { new Guid("217f6d72-a33e-4612-b164-f1bbd5db94c2"), 0, new Guid("5fd3e0a3-0e0e-445a-93e6-8f94b6690794"), 5000, "Kingdom1 County1", 0, 1000, 1.0, 2000, 0, 5000, 5 },
                    { new Guid("9160fe49-2966-4fb6-94d7-6999c7351368"), 0, new Guid("5fd3e0a3-0e0e-445a-93e6-8f94b6690794"), 5000, "Kingdom1 County2", 0, 1000, 1.0, 2000, 0, 5000, 5 }
                });

            migrationBuilder.InsertData(
                table: "Technologies",
                columns: new[] { "Id", "KingdomId", "Status", "TechnologySpecificsId" },
                values: new object[,]
                {
                    { new Guid("fce4e719-9a94-4a29-b9da-3ab5b87a910e"), new Guid("5fd3e0a3-0e0e-445a-93e6-8f94b6690794"), 1, new Guid("4e9f32b6-2621-4f7c-a939-f4d1a1a2daae") },
                    { new Guid("605b6d6a-40dd-415d-8726-46ef8c0592bf"), new Guid("a37de913-486d-4df3-9025-1e5d4f881220"), 3, new Guid("93ad7e45-7071-48d5-a5df-c5eb21bb35da") },
                    { new Guid("3444c5c0-db00-49fa-a0be-676e4d05864a"), new Guid("5fd3e0a3-0e0e-445a-93e6-8f94b6690794"), 1, new Guid("93ad7e45-7071-48d5-a5df-c5eb21bb35da") },
                    { new Guid("e12a9bfd-1847-4e43-ae91-bcce2c5545a3"), new Guid("a37de913-486d-4df3-9025-1e5d4f881220"), 3, new Guid("4e9f32b6-2621-4f7c-a939-f4d1a1a2daae") },
                    { new Guid("fb6408b3-d806-43f1-9ee4-ffefc885e9ce"), new Guid("5fd3e0a3-0e0e-445a-93e6-8f94b6690794"), 1, new Guid("f7f7f6a9-1ce5-4051-82b0-a55fb19d901c") },
                    { new Guid("0d5fa209-cd89-4f26-bf6c-2f915a661226"), new Guid("a37de913-486d-4df3-9025-1e5d4f881220"), 3, new Guid("a6336474-fa17-43ba-a5c6-7fee92ab15b7") },
                    { new Guid("0bfc558f-e32b-463e-9e74-06696bd7877a"), new Guid("5fd3e0a3-0e0e-445a-93e6-8f94b6690794"), 1, new Guid("a6336474-fa17-43ba-a5c6-7fee92ab15b7") },
                    { new Guid("67648201-cb7a-4904-8622-b2863444eb82"), new Guid("a37de913-486d-4df3-9025-1e5d4f881220"), 3, new Guid("f7f7f6a9-1ce5-4051-82b0-a55fb19d901c") }
                });

            migrationBuilder.InsertData(
                table: "UnitLevels",
                columns: new[] { "Id", "ForceLimit", "GoldCost", "GoldUpkeep", "Level", "MarbleCost", "MarbleUpkeep", "MeleeAttackPower", "MeleeDefensePower", "RangedAttackPower", "RangedDefensePower", "SulfurCost", "SulfurUpkeep", "UnitSpecificsId", "WineCost", "WineUpkeep", "WoodCost", "WoodUpkeep" },
                values: new object[,]
                {
                    { new Guid("cc847345-0747-45ee-ab9b-60a97873d9b1"), 10, 200, 50, 2, 100, 40, 0, 0, 40, 2, 100, 0, new Guid("06d69f35-d7f8-444f-bd25-da45bc6accb6"), 0, 0, 200, 100 },
                    { new Guid("3e1869eb-f12a-40e7-bec3-0b3ae410370a"), 1, 10, 5, 1, 0, 0, 5, 5, 0, 3, 0, 0, new Guid("97f6314a-766d-4aa2-9c49-7dbcf86140b5"), 0, 0, 15, 0 },
                    { new Guid("26f967a9-44b9-4aa9-88ae-b3af7ac0ad09"), 2, 15, 10, 2, 5, 0, 6, 6, 0, 7, 0, 0, new Guid("97f6314a-766d-4aa2-9c49-7dbcf86140b5"), 0, 0, 25, 0 },
                    { new Guid("15ec08c3-a2e0-42a9-bd5a-37381e121dcb"), 3, 30, 15, 3, 50, 0, 12, 12, 0, 10, 0, 0, new Guid("97f6314a-766d-4aa2-9c49-7dbcf86140b5"), 0, 0, 50, 0 },
                    { new Guid("2e560b5e-f626-4b25-8957-0337f385c2be"), 2, 10, 5, 1, 10, 5, 1, 1, 5, 2, 0, 0, new Guid("489e9070-f6f6-4130-8979-89e54b140835"), 0, 0, 15, 0 },
                    { new Guid("2c944d6f-a2ea-448a-a4f8-7756ef115263"), 3, 20, 10, 2, 15, 10, 2, 2, 10, 4, 0, 0, new Guid("489e9070-f6f6-4130-8979-89e54b140835"), 0, 0, 30, 0 },
                    { new Guid("b0563dec-ae9a-49df-bbc2-4db74bf5e2ec"), 4, 35, 20, 3, 20, 15, 3, 3, 15, 6, 0, 0, new Guid("489e9070-f6f6-4130-8979-89e54b140835"), 0, 0, 50, 0 },
                    { new Guid("e8cfbc91-d0c7-4ee4-a123-ce3e554026f8"), 5, 100, 20, 1, 50, 20, 0, 0, 20, 2, 50, 0, new Guid("06d69f35-d7f8-444f-bd25-da45bc6accb6"), 0, 0, 100, 50 },
                    { new Guid("9946dd5f-078a-4029-9ba1-0a3b22d8dfdc"), 15, 400, 100, 3, 200, 80, 0, 0, 80, 2, 200, 0, new Guid("06d69f35-d7f8-444f-bd25-da45bc6accb6"), 0, 0, 400, 200 }
                });

            migrationBuilder.InsertData(
                table: "Buildings",
                columns: new[] { "Id", "BuildingSpecificsId", "CountyId", "Level", "Status" },
                values: new object[,]
                {
                    { new Guid("a3cf5dd1-478a-4ea2-a049-6bbb7f965863"), new Guid("e2bfc4a7-d73f-4a2e-b91f-209c08a3f14f"), new Guid("217f6d72-a33e-4612-b164-f1bbd5db94c2"), 1, 1 },
                    { new Guid("9be0efdd-f662-4c7e-8ae3-6cb576f3ea9e"), new Guid("1d203260-0928-47b6-9d10-5e4cf0c70265"), new Guid("01ef4de3-61c4-4671-bcd3-4b5009dea2d2"), 1, 3 },
                    { new Guid("8a6390d8-abbe-4f41-ba33-ffbae69bb183"), new Guid("e2bfc4a7-d73f-4a2e-b91f-209c08a3f14f"), new Guid("01ef4de3-61c4-4671-bcd3-4b5009dea2d2"), 1, 3 },
                    { new Guid("cd118d28-dc24-44ca-97c7-af2c6f53aa58"), new Guid("d02e3c9c-f26c-4136-a904-27ad074fa456"), new Guid("610fb8b0-386e-4b0d-9a51-59403fd686b6"), 1, 1 },
                    { new Guid("61156095-4f2f-4d44-8fc4-e15b278557a2"), new Guid("4db1c8d2-b2b0-49a9-b8a5-8f9d5bbecddb"), new Guid("610fb8b0-386e-4b0d-9a51-59403fd686b6"), 1, 1 },
                    { new Guid("e52bd2ac-c58e-4277-a622-545c257fd1d9"), new Guid("1d203260-0928-47b6-9d10-5e4cf0c70265"), new Guid("610fb8b0-386e-4b0d-9a51-59403fd686b6"), 1, 1 },
                    { new Guid("fbcc61c5-8886-4703-ae57-8406be7c0713"), new Guid("e2bfc4a7-d73f-4a2e-b91f-209c08a3f14f"), new Guid("610fb8b0-386e-4b0d-9a51-59403fd686b6"), 1, 1 },
                    { new Guid("2f7b1592-f75f-461b-a497-6344b64737a4"), new Guid("4db1c8d2-b2b0-49a9-b8a5-8f9d5bbecddb"), new Guid("01ef4de3-61c4-4671-bcd3-4b5009dea2d2"), 1, 3 },
                    { new Guid("cff226fd-aa32-4942-a489-0d0dcb82cbae"), new Guid("d02e3c9c-f26c-4136-a904-27ad074fa456"), new Guid("9160fe49-2966-4fb6-94d7-6999c7351368"), 1, 3 },
                    { new Guid("9dbb0a30-8b47-4843-9c03-06e7b4c501cf"), new Guid("1d203260-0928-47b6-9d10-5e4cf0c70265"), new Guid("9160fe49-2966-4fb6-94d7-6999c7351368"), 1, 3 },
                    { new Guid("326740fa-a052-4b34-9038-b825d9d3d5cb"), new Guid("e2bfc4a7-d73f-4a2e-b91f-209c08a3f14f"), new Guid("9160fe49-2966-4fb6-94d7-6999c7351368"), 1, 3 },
                    { new Guid("50c2da0b-43a4-42ae-97ab-c89009f7dd8d"), new Guid("d02e3c9c-f26c-4136-a904-27ad074fa456"), new Guid("217f6d72-a33e-4612-b164-f1bbd5db94c2"), 1, 1 },
                    { new Guid("5b24eb70-9a6e-40de-b620-96f6dca50fab"), new Guid("4db1c8d2-b2b0-49a9-b8a5-8f9d5bbecddb"), new Guid("217f6d72-a33e-4612-b164-f1bbd5db94c2"), 1, 1 },
                    { new Guid("06ebbbb8-7154-4419-aea5-46d327594517"), new Guid("1d203260-0928-47b6-9d10-5e4cf0c70265"), new Guid("217f6d72-a33e-4612-b164-f1bbd5db94c2"), 1, 1 },
                    { new Guid("ac2dbc91-0163-4064-a6a9-a13afaad7ced"), new Guid("4db1c8d2-b2b0-49a9-b8a5-8f9d5bbecddb"), new Guid("9160fe49-2966-4fb6-94d7-6999c7351368"), 1, 3 },
                    { new Guid("19d2606c-9e43-45a8-8631-2dfa58361822"), new Guid("d02e3c9c-f26c-4136-a904-27ad074fa456"), new Guid("01ef4de3-61c4-4671-bcd3-4b5009dea2d2"), 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "UnitGroups",
                columns: new[] { "Id", "AttackId", "CountyId" },
                values: new object[,]
                {
                    { new Guid("05926428-3186-4730-85c6-31b740cd9e5d"), null, new Guid("217f6d72-a33e-4612-b164-f1bbd5db94c2") },
                    { new Guid("1a4b7681-b373-420d-aaa0-c5ec80e00b16"), null, new Guid("610fb8b0-386e-4b0d-9a51-59403fd686b6") }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Level", "UnitGroupId", "UnitSpecificsId" },
                values: new object[,]
                {
                    { new Guid("9d7f181c-8933-483d-af15-d50a0a599c87"), 1, new Guid("05926428-3186-4730-85c6-31b740cd9e5d"), new Guid("97f6314a-766d-4aa2-9c49-7dbcf86140b5") },
                    { new Guid("6edbded8-6fe4-4f84-b7ac-1a4ea8edce6b"), 3, new Guid("05926428-3186-4730-85c6-31b740cd9e5d"), new Guid("97f6314a-766d-4aa2-9c49-7dbcf86140b5") },
                    { new Guid("3454f1bd-a857-401d-9da7-c51ba635f198"), 1, new Guid("05926428-3186-4730-85c6-31b740cd9e5d"), new Guid("489e9070-f6f6-4130-8979-89e54b140835") },
                    { new Guid("2229d5a6-09cb-4e40-93b9-3a2367d3b645"), 2, new Guid("05926428-3186-4730-85c6-31b740cd9e5d"), new Guid("06d69f35-d7f8-444f-bd25-da45bc6accb6") },
                    { new Guid("5ff8abb8-6cf9-4c16-84fb-b8bd4f6e6741"), 1, new Guid("1a4b7681-b373-420d-aaa0-c5ec80e00b16"), new Guid("97f6314a-766d-4aa2-9c49-7dbcf86140b5") },
                    { new Guid("abd37410-d250-4e8c-8e5c-256ae2836f36"), 2, new Guid("1a4b7681-b373-420d-aaa0-c5ec80e00b16"), new Guid("489e9070-f6f6-4130-8979-89e54b140835") },
                    { new Guid("7a904252-9830-45f6-949d-b742c210975b"), 2, new Guid("1a4b7681-b373-420d-aaa0-c5ec80e00b16"), new Guid("489e9070-f6f6-4130-8979-89e54b140835") },
                    { new Guid("fa09b553-57f6-4f3a-ac29-acaec501d22c"), 3, new Guid("1a4b7681-b373-420d-aaa0-c5ec80e00b16"), new Guid("06d69f35-d7f8-444f-bd25-da45bc6accb6") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_GameId",
                table: "AspNetUsers",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_KingdomId",
                table: "AspNetUsers",
                column: "KingdomId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_AttackerId",
                table: "Attacks",
                column: "AttackerId");

            migrationBuilder.CreateIndex(
                name: "IX_Attacks_DefenderId",
                table: "Attacks",
                column: "DefenderId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingLevels_BuildingSpecificsId",
                table: "BuildingLevels",
                column: "BuildingSpecificsId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_BuildingSpecificsId",
                table: "Buildings",
                column: "BuildingSpecificsId");

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_CountyId",
                table: "Buildings",
                column: "CountyId");

            migrationBuilder.CreateIndex(
                name: "IX_Counties_KingdomId",
                table: "Counties",
                column: "KingdomId");

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_KingdomId",
                table: "Technologies",
                column: "KingdomId");

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_TechnologySpecificsId",
                table: "Technologies",
                column: "TechnologySpecificsId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitGroups_AttackId",
                table: "UnitGroups",
                column: "AttackId",
                unique: true,
                filter: "[AttackId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UnitGroups_CountyId",
                table: "UnitGroups",
                column: "CountyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UnitLevels_UnitSpecificsId",
                table: "UnitLevels",
                column: "UnitSpecificsId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_UnitGroupId",
                table: "Units",
                column: "UnitGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_UnitSpecificsId",
                table: "Units",
                column: "UnitSpecificsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BuildingLevels");

            migrationBuilder.DropTable(
                name: "Buildings");

            migrationBuilder.DropTable(
                name: "Technologies");

            migrationBuilder.DropTable(
                name: "UnitLevels");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BuildingSpecifics");

            migrationBuilder.DropTable(
                name: "TechnologySpecifics");

            migrationBuilder.DropTable(
                name: "UnitGroups");

            migrationBuilder.DropTable(
                name: "UnitSpecifics");

            migrationBuilder.DropTable(
                name: "Game");

            migrationBuilder.DropTable(
                name: "Attacks");

            migrationBuilder.DropTable(
                name: "Counties");

            migrationBuilder.DropTable(
                name: "Kingdoms");
        }
    }
}
