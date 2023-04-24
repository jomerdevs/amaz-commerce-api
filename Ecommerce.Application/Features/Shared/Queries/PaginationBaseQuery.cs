using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Shared.Queries
{
    public class PaginationBaseQuery
    {
        public string? Search { get; set; }
        public string? Sort { get; set; }
        public int PageIndex { get; set; } = 1;

        private int _pagesize = 3;

        private const int MaxPageSize = 50;

        public int Pagesize
        {
            get => _pagesize;
            set => _pagesize = (value > MaxPageSize) ? MaxPageSize : value;
        }

    }
}
