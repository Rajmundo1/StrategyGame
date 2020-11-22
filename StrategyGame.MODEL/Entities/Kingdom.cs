﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;
using StrategyGame.MODEL.Entities.Technologies;
using StrategyGame.MODEL.Enums;

namespace StrategyGame.MODEL.Entities
{
    public class Kingdom
    {
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public IEnumerable<County> Counties { get; set; }
        public IEnumerable<Technology> Technologies { get; set; }

        public int Gold { get; set; }
        [NotMapped]
        public int GlobalGoldIncome => Counties.Sum(county => county.GoldIncome + county.GoldIncomeBonus);

        public int ResearchPoint { get; set; }
        [NotMapped]
        public int GlobalResearchOutput => Counties.Sum(county => county.ResearchOutput + county.ResearchOutPutBonus);

        [NotMapped]
        public int GlobalScore => Counties.Sum(county => county.Score) + Technologies.Sum(technology => technology.Status == ResearchStatus.Researched ? 3 : 0);
    }
}
