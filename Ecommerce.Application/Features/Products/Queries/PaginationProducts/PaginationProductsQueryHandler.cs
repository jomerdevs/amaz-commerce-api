using AutoMapper;
using Ecommerce.Application.Features.Products.Queries.ViewModels;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Application.Pagination.Products;
using Ecommerce.Application.Repository;
using Ecommerce.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Products.Queries.PaginationProducts
{
    public class PaginationProductsQueryHandler : IRequestHandler<PaginationProductsQuery, PaginationViewModel<ProductViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaginationProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationViewModel<ProductViewModel>> Handle(PaginationProductsQuery request, CancellationToken cancellationToken)
        {
            var productPaginationParams = new ProductPaginationParams
            {
                PageIndex = request.PageIndex,
                PageSize = request.Pagesize,
                Search = request.Search,
                Sort = request.Sort!,
                CategoryId = request.CategoryId,
                PrecioMax = request.PrecioMax,
                PrecioMin = request.PrecioMin,
                Rating = request.Rating,
                Status = request.Status
            };

            var pag = new ProductPagination(productPaginationParams);

            var products = await _unitOfWork.Repository<Product>().GetAllWithSpec(pag);

            var pagCount = new ProductforCountingPagination(productPaginationParams);
            var totalProducts = await _unitOfWork.Repository<Product>().CountAsync(pagCount);

            var rounded = Math.Ceiling(Convert.ToDecimal(totalProducts) / Convert.ToDecimal(request.Pagesize));
            var totalPages = Convert.ToInt32(rounded);

            // --- convertirlo a ViewModel ---
            var data = _mapper.Map<IReadOnlyList<ProductViewModel>>(products);

            var productByPage = products.Count();

            var pagination = new PaginationViewModel<ProductViewModel>
            {
                Count = totalProducts,
                Data = data,
                PageCount = totalPages,
                PageIndex = request.PageIndex,
                PageSize = request.Pagesize,
                ResultByPage = productByPage
            };

            return pagination;
        }
    }
}
