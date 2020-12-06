using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL.Dtos
{
    public class UnitSpecificsDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Level { get; set; }

        public int AttackPower { get; set; }
        public int DefensePower { get; set; }

        public int ForceLimit { get; set; }

        public int WoodCost { get; set; }
        public int MarbleCost { get; set; }
        public int WineCost { get; set; }
        public int SulfurCost { get; set; }
        public int GoldCost { get; set; }

        public int WoodUpkeep { get; set; }
        public int MarbleUpkeep { get; set; }
        public int WineUpkeep { get; set; }
        public int SulfurUpkeep { get; set; }
        public int GoldUpkeep { get; set; }
    }
}
