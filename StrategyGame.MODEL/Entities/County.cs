using StrategyGame.MODEL.Entities.Buildings;
using StrategyGame.MODEL.Entities.Technologies;
using StrategyGame.MODEL.Entities.Units;
using StrategyGame.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace StrategyGame.MODEL.Entities
{
    public class County
    {
        public Guid Id { get; set; }
        [ForeignKey("Kingdom")]
        public Guid KingdomId { get; set; }
        public Kingdom Kingdom {get;set;}
        public string Name { get; set; }
        [NotMapped]
        public int PopulationGrowth => Convert.ToInt32(Math.Floor(BasePopulation * Morale));
        [NotMapped]
        public int ResearchOutput => Buildings.Sum(building => building.CurrentLevel.ResearchOutPut);
        public double TaxRate { get; set; }
        [NotMapped]
        public double Morale => 1.25 - Math.Log10(OverallPopulation)/10 + TavernMorale - (1 - TaxRate);
        [NotMapped]
        public double TavernMorale => Math.Log10(WineConsumption)/10;
        public int WineConsumption { get; set; }
        public int Wood { get; set; }
        [NotMapped]
        public int WoodProduction => Buildings.Sum(building => building.CurrentLevel.WoodProduction);
        public int Marble { get; set; }
        [NotMapped]
        public int MarbleProduction => Buildings.Sum(building => building.CurrentLevel.MarbleProduction);
        public int Wine { get; set; }
        [NotMapped]
        public int WineProduction => Buildings.Sum(building => building.CurrentLevel.WineProduction);
        public int Sulfur { get; set; }
        [NotMapped]
        public int SulfurProduction => Buildings.Sum(building => building.CurrentLevel.SulfurProduction);
        public int BasePopulation { get; set; }
        [NotMapped]
        public int Populationbonus => Buildings.Sum(building => building.CurrentLevel.PopulationBonus);
        [NotMapped]
        public int OverallPopulation => Populationbonus + BasePopulation;
        public int taxPerPop { get; set; }
        [NotMapped]
        public int GoldIncome => Convert.ToInt32(Math.Floor(OverallPopulation * TaxRate));
        [NotMapped]
        public int ForceLimitUsed => Units.Units.Sum(x => x.CurrentLevel.ForceLimit);
        [NotMapped]
        public int ForceLimit => Buildings.Sum(bulding => bulding.CurrentLevel.ForceLimitBonus);
        [NotMapped]
        public int Score => OverallPopulation + Buildings.Sum(building => building.Status == BuildingStatus.Built ? 2 : 0);
        public IEnumerable<Building> Buildings { get; set; }
        public UnitGroup Units { get; set; }

    }
}
