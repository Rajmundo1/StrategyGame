using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.MODEL.Entities.Units
{
    public class UnitLevel
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public int RangedAttackPower { get; set; }
        public int RangedDefensePower { get; set; }
        public int MeleeAttackPower { get; set; }
        public int MeleeDefensePower { get; set; }
        public int Price { get; set; }
        public int UpgradeCost { get; set; }
    }
}
