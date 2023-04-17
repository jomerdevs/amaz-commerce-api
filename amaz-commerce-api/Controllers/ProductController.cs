using Ecommerce.Application.Features.Products.Queries.GetProductList;
using Ecommerce.Application.Features.Products.Queries.ViewModels;
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
    }
}
