using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace StrategyGame.MODEL.Entities.Buildings
{
    public class BuildingSpecifics
    {
        public int Id { get; set; }
        [ForeignKey("Building")]
        public int BuildingId { get; set; }
        public int Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Level { get; set; }
        public int MaxLevel { get; set; }
        [NotMapped]
        public BuildingLevel CurrentLevel => BuildingLevels.First(x => x.Level == Level);
        public IEnumerable<BuildingLevel> BuildingLevels { get; set; }
    }
}
