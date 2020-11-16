using StrategyGame.MODEL.Entities.Units;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StrategyGame.MODEL.Entities
{
    public class Attack
    {
        public int Id { get; set; }
        [ForeignKey("County")]
        public County Attacker { get; set; }
        [ForeignKey("County")]
        public County Defender { get; set; }
        public IEnumerable<Unit> AttackerUnits { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
