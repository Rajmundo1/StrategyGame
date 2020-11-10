using StrategyGame.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.MODEL.Entities.Technologies
{
    public class TechnologySpecifics
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public ResearchStatus Status { get; set; }
    }
}
