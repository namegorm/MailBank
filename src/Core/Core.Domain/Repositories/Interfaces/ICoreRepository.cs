using System;
using System.Linq;
using System.Linq.Expressions;

using Core.Domain.Entities.Interfaces;

namespace Core.Domain.Repositories.Interfaces
{
    public interface ICoreRepository<TEntity>
        where TEntity : ICoreEntity
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = default);
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Delete(long id);
    }
}
