using System;

using Core.Infrastructure.Persistence.DatabaseContextFactories.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Core.Infrastructure.Persistence.DatabaseContextFactories.Implementations
{
    public abstract class CoreDatabaseContextFactory<TDatabaseContext> : ICoreDesignTimeDatabaseContextFactory<TDatabaseContext>
        where TDatabaseContext : DbContext
    {
        protected IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        public abstract TDatabaseContext CreateDbContext(string[] args);
    }
}
