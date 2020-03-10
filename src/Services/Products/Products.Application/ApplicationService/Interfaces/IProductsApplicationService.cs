
using Core.Application.ApplicationServices.Interfaces;

using Products.Application.ViewModels;
using Products.Domain.Entities;

namespace Products.Application.ApplicationService.Interfaces
{
    public interface IProductsApplicationService : ICoreApplicationService<Product, ProductViewModel>
    {
    }
}
