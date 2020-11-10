using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.MODEL.Entities.Technologies
{
    public class Technology
    {
        public int Id { get; set; }
        public TechnologySpecifics Specifics { get; set; }
    }
}
