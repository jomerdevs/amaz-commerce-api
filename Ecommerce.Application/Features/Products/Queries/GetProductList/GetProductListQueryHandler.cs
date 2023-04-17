using AutoMapper;
using Ecommerce.Application.Features.Products.Queries.ViewModels;
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
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IReadOnlyList<ProductViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductViewModel>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Product, object>>>();
            includes.Add(p => p.Images!);
            includes.Add(p => p.Reviews!);

            var products = await _unitOfWork.Repository<Product>().GetAsync(
                null,
                x => x.OrderBy(y => y.Nombre),
                includes,
                true);

            var productVM = _mapper.Map<IReadOnlyList<ProductViewModel>>(products);

            return productVM;
        }
    }
}
