using StrategyGame.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StrategyGame.MODEL.Entities.Technologies
{
    public class TechnologySpecifics
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }

        public int WoodBonus { get; set; }
        public int StoneBonus { get; set; }
        public int WineBonus { get; set; }
        public int SulfurBonus { get; set; }
        public int GoldBonus { get; set; }
        public int ResearchBonus { get; set; }
        public int BuildingTimeBonus { get; set; }
        public int AttackPowerBonus { get; set; }
        public int DefensePowerBonus { get; set; }
    }
}
