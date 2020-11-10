using StrategyGame.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.MODEL.Entities.Buildings
{
    public class Building
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public int Level { get; set; }
        public BuildingStatus Status { get; set; }
    }
}
