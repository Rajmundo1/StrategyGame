using Microsoft.EntityFrameworkCore;
using StrategyGame.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyGame.DAL.Extensions
{
    public static class PagedListExtension
    {
        public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();

            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 15 : pageSize;

            var items = await source.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
