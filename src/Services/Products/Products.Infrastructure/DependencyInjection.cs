using System.Reflection;

using Core.Domain.UnitOfWork.Interfaces;
using Core.Infrastructure.Persistence.DatabaseContexts.Interfaces;
using Core.Infrastructure.UnitOfWork.Implementations;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Products.Domain.Repositories.Interfaces;
using Products.Infrastructure.Persistence.DatabaseContexts.Implementations;
using Products.Infrastructure.Repositories.Implementations;

namespace Products.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(
                x => x.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                y => y.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));

            services.AddScoped<ICoreDatabaseContext>(x => x.GetService<DatabaseContext>());
            services.AddScoped<ICoreUnitOfWork, CoreUnitOfWork>();
            services.AddTransient<IProductsRepository, ProductsRepository>();

            return services;
        }
    }
}
