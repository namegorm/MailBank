using System.Reflection;

using Core.Infrastructure.Persistence.DatabaseContexts.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistence.DatabaseContexts.Implementations
{
    public abstract class CoreDatabaseContext : DbContext, ICoreDatabaseContext
    {
        protected CoreDatabaseContext(DbContextOptions dbContextOptions)
            : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected virtual Assembly GetExecutingAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
    }
}
