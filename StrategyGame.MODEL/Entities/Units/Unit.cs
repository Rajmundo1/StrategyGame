using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StrategyGame.MODEL.Entities.Units
{
    public class Unit
    {
        public int Id { get; set; }
        [ForeignKey("Attack")]
        public int? AttackId { get; set; }
        [ForeignKey("County")]
        public int CountyId { get; set; }
        [ForeignKey("UnitSpecifics")]
        public int UnitSpecificsId { get; set; }
        public UnitSpecifics UnitSpecifics { get; set; }
    }
}
