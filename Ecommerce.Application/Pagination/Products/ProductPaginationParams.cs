using Ecommerce.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Pagination.Products
{
    public class ProductPaginationParams: PaginationParams
    {
        public int? CategoryId { get; set; }
        public decimal? PrecioMax { get; set; }
        public decimal? PrecioMin { get; set; }
        public int? Rating { get; set; }
        public ProductStatus? Status { get; set; }
    }
}
