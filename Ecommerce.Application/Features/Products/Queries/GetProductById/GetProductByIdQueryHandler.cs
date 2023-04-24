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

namespace Ecommerce.Application.Features.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductViewModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var includes = new List<Expression<Func<Product, object>>>();
            includes.Add(p => p.Images!);
            includes.Add(p => p.Reviews!.OrderByDescending(x => x.CreatedDate));

            var product = await _unitOfWork.Repository<Product>().GetEntityAsync(
                    x => x.Id == request.ProductId,
                    includes,
                    true
                );

            return _mapper.Map<ProductViewModel>( product );
        }
    }
}
