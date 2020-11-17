using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace StrategyGame.MODEL.Entities.Units
{
    public class Unit
    {
        public Guid Id { get; set; }
        [ForeignKey("UnitGroup")]
        public Guid UnitGroupId { get; set; }
        [ForeignKey("UnitSpecifics")]
        public Guid UnitSpecificsId { get; set; }
        public int Level { get; set; }
        [NotMapped]
        public UnitLevel CurrentLevel => UnitSpecifics.UnitLevels.First(x => x.Level == Level);
        public UnitSpecifics UnitSpecifics { get; set; }
    }
}
