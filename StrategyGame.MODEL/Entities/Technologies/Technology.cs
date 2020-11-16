using StrategyGame.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StrategyGame.MODEL.Entities.Technologies
{
    public class Technology
    {
        public Guid Id { get; set; }
        [ForeignKey("Kingdom")]
        public Guid KingdomId { get; set; }
        [ForeignKey("TechnologySpecifics")]
        public Guid TechnologySpecificsId { get; set; }
        public TechnologySpecifics Specifics { get; set; }
        public ResearchStatus Status { get; set; }
    }
}
