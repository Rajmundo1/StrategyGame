using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StrategyGame.MODEL.Entities.Buildings
{
    public class BuildingLevel
    {
        public Guid Id { get; set; }
        [ForeignKey("BuildingSpecifics")]
        public Guid BuildingSpecificsId { get; set; }
        public int Level { get; set; }

        public int PopulationBonus { get; set; }
        public int ForceLimitBonus { get; set; }
        public int WoodProduction { get; set; }
        public int MarbleProduction { get; set; }
        public int WineProduction { get; set; }
        public int SulfurProduction { get; set; }
        public int ResearchOutPut { get; set; }

        public int WoodCost { get; set; }
        public int MarbleCost { get; set; }
        public int WineCost { get; set; }
        public int SulfurCost { get; set; }
        public int GoldCost { get; set; }

    }
}
