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
        public int ResearchOutput => Buildings.Sum(building => building.CurrentLevel.ResearchOutPut);
        [NotMapped]
        public int ResearchOutPutBonus => Convert.ToInt32(ResearchOutput * Kingdom.Technologies.Sum(tech => tech.Specifics.ResearchBonus));


        public double TaxRate { get; set; }
        [NotMapped]
        public double Morale => 1.25 - Math.Log10(OverallPopulation)/10 + TavernMorale - (1 - TaxRate);
        [NotMapped]
        public double TavernMorale => Math.Log10(WineConsumption)/10;
        public int WineConsumption { get; set; }

        public int Wood { get; set; }
        [NotMapped]
        public int WoodProduction => Buildings.Sum(building => building.CurrentLevel.WoodProduction);
        [NotMapped]
        public int WoodProductionBonus => Convert.ToInt32(WoodProduction * Kingdom.Technologies.Sum(tech => tech.Specifics.WoodBonus));

        public int Marble { get; set; }
        [NotMapped]
        public int MarbleProduction => Buildings.Sum(building => building.CurrentLevel.MarbleProduction);
        [NotMapped]
        public int MarbleProductionBonus => Convert.ToInt32(MarbleProduction * Kingdom.Technologies.Sum(tech => tech.Specifics.StoneBonus));

        public int Wine { get; set; }
        [NotMapped]
        public int WineProduction => Buildings.Sum(building => building.CurrentLevel.WineProduction);
        [NotMapped]
        public int WineProductionBonus => Convert.ToInt32(WineProduction * Kingdom.Technologies.Sum(tech => tech.Specifics.WineBonus));

        public int Sulfur { get; set; }
        [NotMapped]
        public int SulfurProduction => Buildings.Sum(building => building.CurrentLevel.SulfurProduction);
        [NotMapped]
        public int SulfurProductionBonus => Convert.ToInt32(SulfurProduction * Kingdom.Technologies.Sum(tech => tech.Specifics.SulfurBonus));

        [NotMapped]
        public int GoldIncome => Convert.ToInt32(Math.Floor(OverallPopulation * TaxRate));
        [NotMapped]
        public int GoldIncomeBonus => Convert.ToInt32(Math.Floor(GoldIncome * Kingdom.Technologies.Sum(tech => tech.Specifics.GoldBonus)));

        public int BasePopulation { get; set; }
        [NotMapped]
        public int Populationbonus => Buildings.Sum(building => building.CurrentLevel.PopulationBonus);

        [NotMapped]
        public int PopulationGrowth => Convert.ToInt32(Math.Floor(BasePopulation * Morale));
        [NotMapped]
        public int OverallPopulation => Populationbonus + BasePopulation;
        public int taxPerPop { get; set; }


        [NotMapped]
        public int UsedForceLimit => Units.Units.Sum(x => x.CurrentLevel.ForceLimit);
        [NotMapped]
        public int MaxForceLimit => Buildings.Sum(bulding => bulding.CurrentLevel.ForceLimitBonus);


        [NotMapped]
        public int Score => OverallPopulation + Buildings.Sum(building => building.Status == BuildingStatus.Built ? 2 : 0);
        public IEnumerable<Building> Buildings { get; set; }
        public UnitGroup Units { get; set; }

    }
}
