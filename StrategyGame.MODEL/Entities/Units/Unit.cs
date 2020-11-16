using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public UnitSpecifics UnitSpecifics { get; set; }
    }
}
