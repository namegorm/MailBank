using System.Threading.Tasks;

using Core.Domain.UnitOfWork.Interfaces;
using Core.Infrastructure.Persistence.DatabaseContexts.Interfaces;

namespace Core.Infrastructure.UnitOfWork.Implementations
{
    public class CoreUnitOfWork : ICoreUnitOfWork
    {
        protected ICoreDatabaseContext DatabaseContext { get; }

        public CoreUnitOfWork(ICoreDatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public async Task<int> CommitAsync()
        {
            var saveChangesResult = await DatabaseContext.SaveChangesAsync();
            return saveChangesResult;
        }
    }
}
