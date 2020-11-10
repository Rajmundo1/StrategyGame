using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.MODEL.FilterParameters
{
    public class PagingParameters
    {
        protected int pageNumber = 1;
        protected const int maxPageSize = 50;
        protected int pageSize = 10;

        public int PageNumber
        {
            get
            {
                return pageNumber;
            }
            set
            {
                if (value > 0)
                {
                    pageNumber = value;
                }
            }
        }
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                if (value > 0)
                {
                    pageSize = (value > maxPageSize) ? maxPageSize : value;
                }
            }
        }
    }
}
