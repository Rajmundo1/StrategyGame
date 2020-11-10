using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;
using StrategyGame.MODEL.Entities.Technologies;

namespace StrategyGame.MODEL.Entities
{
    public class Kingdom
    {
        public int Id { get; set; }
        public List<County> Counties { get; set; }
        public int Gold { get; set; }
        public List<Technology> Technology { get; set; }
        [NotMapped]
        public int GlobalGoldIncome => Counties.Sum(county => county.GoldIncome);
        [NotMapped]
        public int GlobalResearchOutput => Counties.Sum(county => county.ResearchOutput);
        [NotMapped]
        public int GlobalResearchPopulation => Counties.Sum(county => county.Population);
    }
}
