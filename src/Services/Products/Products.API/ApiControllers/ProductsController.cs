using System.Threading.Tasks;

using Core.API.ApiControllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Products.Application.ApplicationService.Interfaces;
using Products.Application.ViewModels;
using Products.Domain.Entities;

namespace Products.API.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{:apiVersion}/[controller]")]
    public class ProductsController : CoreApiController<Product, ProductViewModel, IProductsApplicationService>
    {
        public ProductsController(IProductsApplicationService applicationService)
            : base(applicationService)
        {
        }

        [AllowAnonymous]
        public override async Task<IActionResult> GetAsync()
        {
            return await base.GetAsync();
        }
    }
}
