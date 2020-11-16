using StrategyGame.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StrategyGame.MODEL.Entities.Buildings
{
    public class Building
    {
        public int Id { get; set; }
        [ForeignKey("County")]
        public int CountyId { get; set; }
        [ForeignKey("BuildingSpecifics")]
        public int BuildingSpecificsId { get; set; }
        public BuildingSpecifics BuildingSpecifics { get; set; }
        public BuildingStatus Status { get; set; }
    }
}
