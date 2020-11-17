using StrategyGame.MODEL.Entities.Units;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StrategyGame.MODEL.Entities
{
    public class Attack
    {
        public Guid Id { get; set; }
        [ForeignKey("County")]
        public Guid AttackerCountyId { get; set; }
        public County Attacker { get; set; }
        [ForeignKey("County")]
        public Guid DefenderCountyId { get; set; }
        public County Defender { get; set; }
        public UnitGroup AttackerUnits { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
