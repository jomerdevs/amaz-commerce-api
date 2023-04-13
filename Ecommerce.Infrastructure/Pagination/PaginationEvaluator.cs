using Ecommerce.Application.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Pagination
{
    public class PaginationEvaluator<T> where T : class
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, IPagination<T> pag) 
        {
            if (pag.Criteria != null) { inputQuery = inputQuery.Where(pag.Criteria); }
            if (pag.OrderBy != null) { inputQuery = inputQuery.OrderBy(pag.OrderBy); }
            if (pag.OrderByDescending != null) { inputQuery = inputQuery.OrderBy(pag.OrderByDescending); }
            if (pag.IsPaginationEnable) { inputQuery = inputQuery.Skip(pag.Skip).Take(pag.Take); }

            inputQuery = pag.Includes!.Aggregate(inputQuery, (current, include) => current.Include(include))
                .AsSplitQuery().AsNoTracking();

            return inputQuery;
        }
    }
}
