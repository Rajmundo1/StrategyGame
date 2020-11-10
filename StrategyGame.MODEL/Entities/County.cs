using StrategyGame.MODEL.Entities.Buildings;
using StrategyGame.MODEL.Entities.Units;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StrategyGame.MODEL.Entities
{
    public class County
    {
        public int Id { get; set; }
        public int Population { get; set; }
        [NotMapped]
        public int PopulationGrowth => Convert.ToInt32(Math.Floor(Population * Morale));
        public int ResearchOutput { get; set; }
        public int GoldIncome => Convert.ToInt32(Math.Floor(Population * TaxRate));
        public double TaxRate { get; set; }
        [NotMapped]
        public double Morale => 1.25 - Math.Log10(Population)/10 + TavernMorale - (1 - TaxRate);
        [NotMapped]
        public double TavernMorale => Math.Log10(WineConsumption)/10;
        public int WineConsumption { get; set; }

        public int Wood { get; set; }
        public int WoodProduction { get; set; }
        public int Marble { get; set; }
        public int MarbleProduction { get; set; }
        public int Wine { get; set; }
        public int WineProduction { get; set; }
        public int Sulfur { get; set; }
        public int SulfurProduction { get; set; }

        public List<Building> Buildings { get; set; }
        public List<Unit> Units { get; set; }

    }
}
