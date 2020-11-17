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
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int MaxLevel { get; set; }
        public IEnumerable<BuildingLevel> BuildingLevels { get; set; }
    }
}
