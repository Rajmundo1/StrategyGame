using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL.Dtos
{
    public class MainPageDto
    {
        public int Round { get; set; }

        public int Gold { get; set; }

        public int Wood { get; set; }
        public int Marble { get; set; }
        public int Wine { get; set; }
        public int Sulfur { get; set; }

        public int WoodProduction { get; set; }
        public int MarbleProduction { get; set; }
        public int WineProduction { get; set; }
        public int SulfurProduction { get; set; }

        public IEnumerable<BuildingDto> Buildings { get; set; }
    }
}
