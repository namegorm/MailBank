
using Core.Domain.Repositories.Interfaces;

using Products.Domain.Entities;

namespace Products.Domain.Repositories.Interfaces
{
    public interface IProductsRepository : ICoreRepository<Product>
    {
    }
}
