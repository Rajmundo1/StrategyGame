﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace StrategyGame.MODEL.Entities.Units
{
    public class UnitSpecifics
    {
        public int Id { get; set; }
        [ForeignKey("Unit")]
        public int UnitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Level { get; set; }
        public int MaxLevel { get; set; }
        [NotMapped]
        public UnitLevel CurrentLevel => UnitLevels.First(x => x.Level == Level);
        public IEnumerable<UnitLevel> UnitLevels { get; set; }
    }
}
