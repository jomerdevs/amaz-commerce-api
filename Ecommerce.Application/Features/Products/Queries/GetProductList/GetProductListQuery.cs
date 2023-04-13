using Ecommerce.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Products.Queries.GetProductList
{
    public class GetProductListQuery : IRequest<List<Product>>
    {
    }
}
