
using Core.Infrastructure.Persistence.DatabaseContextFactories.Implementations;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Products.Infrastructure.Persistence.DatabaseContexts.Implementations;

namespace Products.Infrastructure.Persistence.DatabaseContextFactories.Implementations
{
    public class DatabaseContextFactory : CoreDatabaseContextFactory<DatabaseContext>
    {
        public override DatabaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
