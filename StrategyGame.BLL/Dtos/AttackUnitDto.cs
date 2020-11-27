using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL.Dtos
{
    public class AttackUnitDto
    {
        public Guid UnitSpecificsId { get; set; }
        public int Count { get; set; }
        public int Level { get; set; }
    }
}
