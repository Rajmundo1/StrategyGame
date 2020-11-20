﻿using StrategyGame.MODEL.Entities.Buildings;
using StrategyGame.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL.Dtos
{
    public class BuildingDto
    {
        public Guid Id { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int MaxLevel { get; set; }
        public BuildingStatus Status { get; set; }
    }
}
