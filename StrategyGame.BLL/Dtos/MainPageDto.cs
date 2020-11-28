using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL.Dtos
{
    public class MainPageDto
    {
        public string CurrentCountyName { get; set; }
        public int Round { get; set; }

        public int Gold { get; set; }
        public int GoldIncome { get; set; }
        public int GoldIncomeBonus { get; set; }

        public int ResearchPoint { get; set; }
        public int ResearchOutput { get; set; }
        public int ResearchOutputBonus { get; set; }

        public int Wood { get; set; }
        public int WoodProduction { get; set; }
        public int WoodProductionBonus { get; set; }

        public int Marble { get; set; }
        public int MarbleProduction { get; set; }
        public int MarbleProductionBonus { get; set; }

        public int Wine { get; set; }
        public int WineProduction { get; set; }
        public int WineProductionBonus { get; set; }

        public int Sulfur { get; set; }
        public int SulfurProduction { get; set; }
        public int SulfurProductionBonus { get; set; }

        public int UsedForceLimit { get; set; }
        public int MaxForceLimit { get; set; }

        public string WoodPictureUrl { get; set; }
        public string MarblePictureUrl { get; set; }
        public string WinePictureUrl { get; set; }
        public string SulfurPictureUrl { get; set; }
        public string GoldPictureUrl { get; set; }
        public string TechnologyPictureUrl { get; set; }

        public IEnumerable<BuildingViewDto> Buildings { get; set; }
    }
}
