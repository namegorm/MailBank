using System.Threading.Tasks;

using Core.API.ApiControllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Products.Application.ApplicationService.Interfaces;
using Products.Application.ViewModels;
using Products.Domain.Entities;

namespace Products.API.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{:apiVersion}/[controller]")]
    [Route("api/v{:apiVersion}/product")]
    public class ProductsController : CoreApiController<Product, ProductViewModel, IProductsApplicationService>
    {
        public ProductsController(IProductsApplicationService applicationService, ILogger<ProductsController> logger)
            : base(applicationService, logger)
        {
        }

        [AllowAnonymous]
        public override async Task<IActionResult> GetAsync()
        {
            return await base.GetAsync();
        }

        [AllowAnonymous]
        public override Task<IActionResult> GetAsync(long id)
        {
            return base.GetAsync(id);
        }
    }
}
