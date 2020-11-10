using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StrategyGame.MODEL.Entities.Units
{
    public class Unit
    {
        public int Id { get; set; }
        //public UnitSpecifics Specifics { get; set; }
        public string Name { get; set; }
        public List<UnitLevel> Levels { get; set; }
        public string PictureUrl { get; set; }
        public int CurrentLevel { get; set; }

        [NotMapped]
        public int RangedAttackPower => Levels.Find(level => level.Level == CurrentLevel).RangedAttackPower;
        [NotMapped]
        public int RangedDefensePower => Levels.Find(level => level.Level == CurrentLevel).RangedDefensePower;
        [NotMapped]
        public int MeleeAttackPower => Levels.Find(level => level.Level == CurrentLevel).MeleeAttackPower;
        [NotMapped]
        public int MeleeDefensePower => Levels.Find(level => level.Level == CurrentLevel).MeleeDefensePower;
        [NotMapped]
        public int Price => Levels.Find(level => level.Level == CurrentLevel).Price;

        public int WoodCost { get; set; }
        public int StoneCost { get; set; }
        public int WineCost { get; set; }
        public int SulfurCost { get; set; }
    }
}
