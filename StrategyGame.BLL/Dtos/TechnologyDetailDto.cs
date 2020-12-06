using StrategyGame.MODEL.Entities.Technologies;
using StrategyGame.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL.Dtos
{
    public class TechnologyDetailDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public double WoodBonus { get; set; }
        public double StoneBonus { get; set; }
        public double WineBonus { get; set; }
        public double SulfurBonus { get; set; }
        public double GoldBonus { get; set; }
        public double ResearchBonus { get; set; }
        public double AttackPowerBonus { get; set; }
        public double DefensePowerBonus { get; set; }
        public int ResearchPointCost { get; set; }
        public ResearchStatus Status { get; set; }
    }
}
