using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL.Dtos
{
    public class PagingParametersDto
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
