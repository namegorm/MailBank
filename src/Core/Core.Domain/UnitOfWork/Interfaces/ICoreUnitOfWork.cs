using System.Threading.Tasks;

namespace Core.Domain.UnitOfWork.Interfaces
{
    public interface ICoreUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
