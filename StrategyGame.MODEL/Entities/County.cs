using StrategyGame.MODEL.Entities.Buildings;
using StrategyGame.MODEL.Entities.Technologies;
using StrategyGame.MODEL.Entities.Units;
using StrategyGame.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace StrategyGame.MODEL.Entities
{
    public class County
    {
        public Guid Id { get; set; }
        [ForeignKey("Kingdom")]
        public Guid KingdomId { get; set; }
        public string Name { get; set; }


        [NotMapped]
        public int PopulationGrowth => Convert.ToInt32(Math.Floor(Population * Morale));
        public int ResearchOutput { get; set; }
        public double TaxRate { get; set; }
        [NotMapped]
        public double Morale => 1.25 - Math.Log10(Population)/10 + TavernMorale - (1 - TaxRate);
        [NotMapped]
        public double TavernMorale => Math.Log10(WineConsumption)/10;
        public int WineConsumption { get; set; }

        public int Wood { get; set; }
        [NotMapped]
        public int WoodProduction => Buildings.Sum(building => building.BuildingSpecifics.CurrentLevel.WoodProduction);
        public int Marble { get; set; }
        [NotMapped]
        public int MarbleProduction => Buildings.Sum(building => building.BuildingSpecifics.CurrentLevel.MarbleProduction);
        public int Wine { get; set; }
        [NotMapped]
        public int WineProduction => Buildings.Sum(building => building.BuildingSpecifics.CurrentLevel.WineProduction);
        public int Sulfur { get; set; }
        [NotMapped]
        public int SulfurProduction => Buildings.Sum(building => building.BuildingSpecifics.CurrentLevel.SulfurProduction);
        [NotMapped]
        public int Population => Buildings.Sum(building => building.BuildingSpecifics.CurrentLevel.PopulationBonus);
        public int taxPerPop { get; set; }
        [NotMapped]
        public int GoldIncome => Convert.ToInt32(Math.Floor(Population * TaxRate));
        [NotMapped]
        public int ForceLimit => Buildings.Sum(bulding => bulding.BuildingSpecifics.CurrentLevel.ForceLimitBonus);
        public int ResearchRoundLeft { get; set; }
        public int BuildingRoundLeft { get; set; }
        [NotMapped]
        public int Score => Population + Buildings.Sum(building => building.Status == BuildingStatus.Built ? 2 : 0);

        public IEnumerable<Building> Buildings { get; set; }
        public UnitGroup Units { get; set; }

    }
}
