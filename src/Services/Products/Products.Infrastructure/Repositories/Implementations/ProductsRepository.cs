
using Core.Infrastructure.Persistence.DatabaseContexts.Interfaces;
using Core.Infrastructure.Repositories.Implementations;

using Products.Domain.Entities;
using Products.Domain.Repositories.Interfaces;

namespace Products.Infrastructure.Repositories.Implementations
{
    public class ProductsRepository : CoreRepository<Product>, IProductsRepository
    {
        public ProductsRepository(ICoreDatabaseContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
