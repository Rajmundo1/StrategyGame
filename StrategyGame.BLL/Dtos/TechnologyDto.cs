using StrategyGame.MODEL.Entities.Technologies;
using StrategyGame.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL.Dtos
{
    public class TechnologyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public int ResearchPointCost { get; set; }
        public ResearchStatus Status { get; set; }
    }
}
