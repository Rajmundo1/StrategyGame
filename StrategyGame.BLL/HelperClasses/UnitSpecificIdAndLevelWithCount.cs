using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL.HelperClasses
{
    public class UnitSpecificIdAndLevelWithCount
    {
        public Guid UnitSpecificsId { get; set; }
        public int Level { get; set; }
        public int Count { get; set; }
    }
}
