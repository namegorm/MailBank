using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Core.Domain.Entities.Interfaces;

namespace Core.Application.ApplicationServices.Interfaces
{
    public interface ICoreApplicationService<TEntity, TViewModel>
        where TEntity : class, ICoreEntity
    {
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression = default);
        Task<TEntity> CreateAsync(TViewModel viewModel);
        Task<TEntity> UpdateAsync(TViewModel viewModel);
        Task<TEntity> DeleteAsync(long id);
    }
}
