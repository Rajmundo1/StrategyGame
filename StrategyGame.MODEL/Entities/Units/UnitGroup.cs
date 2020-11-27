using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StrategyGame.MODEL.Entities.Units
{
    public class UnitGroup
    {
        public Guid Id { get; set; }
        [ForeignKey("Attack")]
        public Guid? AttackId { get; set; }
        [ForeignKey("County")]
        public Guid? CountyId { get; set; }
        public IEnumerable<Unit> Units { get; set; }
    }
}
