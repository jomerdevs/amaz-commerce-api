using Ecommerce.Application.Features.Products.Queries.GetProductById;
using Ecommerce.Application.Features.Products.Queries.GetProductList;
using Ecommerce.Application.Features.Products.Queries.PaginationProducts;
using Ecommerce.Application.Features.Products.Queries.ViewModels;
using Ecommerce.Application.Features.Shared.Queries;
using Ecommerce.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace amaz_commerce_api.Controllers
{
    [ApiController]
    [Route("api/v1/products")]
    public class ProductController : ControllerBase
    {
        private IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet("list")]
        [ProducesResponseType(typeof(IReadOnlyList<ProductViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyList<ProductViewModel>>> GetProducts()
        {
            var query = new GetProductListQuery();
            var productList = await _mediator.Send(query);

            return Ok(productList);
        }

        [AllowAnonymous]
        [HttpGet("pagination", Name = "PaginationProduct")]
        [ProducesResponseType(typeof(PaginationViewModel<ProductViewModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginationViewModel<ProductViewModel>>> PaginationProduct(
                [FromQuery] PaginationProductsQuery paginationProductsQuery
            )
        {
            paginationProductsQuery.Status = ProductStatus.Activo;
            var paginationProduct = await _mediator.Send(paginationProductsQuery);

            return Ok(paginationProduct);
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductViewModel>> GetProductById(int id)
        {
            var query = new GetProductByIdQuery(id);

            var data = await _mediator.Send(query);

            return Ok(data);
        }
    }
}
