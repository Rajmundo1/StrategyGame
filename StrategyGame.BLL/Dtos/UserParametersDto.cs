using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL.Dtos
{
    public class UserParametersDto: PagingParametersDto
    {
        public string Name { get; set; }
        public int? ScoreboardPlace { get; set; }
    }
}
