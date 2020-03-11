using AutoMapper;

using Core.Application.ApplicationServices.Implementations;
using Core.Domain.UnitOfWork.Interfaces;

using Microsoft.Extensions.Logging;

using Products.Application.ApplicationService.Interfaces;
using Products.Application.ViewModels;
using Products.Domain.Entities;
using Products.Domain.Repositories.Interfaces;

namespace Products.Application.ApplicationService.Implementations
{
    public class ProductsApplicationService : CoreApplicationService<Product, ProductViewModel, IProductsRepository>, IProductsApplicationService
    {
        public ProductsApplicationService(IProductsRepository linkedRepository, IMapper mapper, ICoreUnitOfWork unitOfWork, ILogger<ProductsApplicationService> logger)
            : base(linkedRepository, mapper, unitOfWork, logger)
        {
        }
    }
}
