using StrategyGame.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL.Dtos
{
    public class BuildingViewDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public BuildingStatus Status { get; set; }
    }
}
