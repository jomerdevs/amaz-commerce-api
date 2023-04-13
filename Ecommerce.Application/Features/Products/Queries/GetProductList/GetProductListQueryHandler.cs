using Ecommerce.Application.Repository;
using Ecommerce.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Products.Queries.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, List<Product>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProductListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Product>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Product, object>>>();
            includes.Add(p => p.Images!);
            includes.Add(p => p.Reviews!);

            var products = await _unitOfWork.Repository<Product>().GetAsync(
                null,
                x => x.OrderBy(y => y.Nombre),
                includes,
                true);

            return new List<Product>(products);
        }
    }
}
