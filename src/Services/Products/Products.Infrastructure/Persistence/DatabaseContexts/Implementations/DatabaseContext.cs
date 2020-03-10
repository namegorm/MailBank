using System.Reflection;

using Core.Infrastructure.Persistence.DatabaseContexts.Implementations;

using Microsoft.EntityFrameworkCore;

namespace Products.Infrastructure.Persistence.DatabaseContexts.Implementations
{
    public class DatabaseContext : CoreDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected override Assembly GetExecutingAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
