using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistence.DatabaseContexts.Interfaces
{
    public interface ICoreDatabaseContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
