using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame.MODEL
{
    public class PagedList<T>: List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList()
        {
        }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            CurrentPage = pageNumber;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((double)TotalCount / pageSize);

            AddRange(items);
        }
    }
}

