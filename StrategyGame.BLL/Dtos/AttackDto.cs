using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL.Dtos
{
    public class AttackDto
    {
        public Guid Id { get; set; }
        public Guid AttackerCountyId { get; set; }
        public Guid DefenderCountyId { get; set; }
        public string DefenderCountyName { get; set; }
        public IEnumerable<UnitDto> Units { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
