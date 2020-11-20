using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;

namespace StrategyGame.BLL.Dtos
{
    public class UnitDto
    {
        public Guid UnitSpecificsId { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int Level { get; set; }
        public int MaxLevel { get; set; }
        public string ImageUrl { get; set; }
    }
}
