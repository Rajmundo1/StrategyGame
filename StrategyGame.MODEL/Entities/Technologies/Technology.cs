using StrategyGame.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StrategyGame.MODEL.Entities.Technologies
{
    public class Technology
    {
        public int Id { get; set; }
        [ForeignKey("Kingdom")]
        public int KingdomId { get; set; }
        public TechnologySpecifics Specifics { get; set; }
        public ResearchStatus Status { get; set; }
    }
}
