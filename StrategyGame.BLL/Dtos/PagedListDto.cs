using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.BLL.Dtos
{
    public class PagedListDto<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public PaginationHeader PaginationHeader { get; set; }
    }
}
