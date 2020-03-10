using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Core.Infrastructure.Persistence.DatabaseContextFactories.Interfaces
{
    public interface ICoreDesignTimeDatabaseContextFactory<TDatabaseContext> : IDesignTimeDbContextFactory<TDatabaseContext>
        where TDatabaseContext : DbContext
    {
    }
}
