using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL.Dtos
{
    public class UserParametersDto: PagingParametersDto
    {
        public string Name { get; set; }
        public int? MinScore { get; set; }
        public int? MaxScore { get; set; }
    }
}
