using System;
using System.Linq;
using System.Linq.Expressions;

using Core.Domain.Entities.Interfaces;
using Core.Domain.Repositories.Interfaces;
using Core.Infrastructure.Persistence.DatabaseContexts.Interfaces;

namespace Core.Infrastructure.Repositories.Implementations
{
    public class CoreRepository<TEntity> : ICoreRepository<TEntity>
        where TEntity : class, ICoreEntity
    {
        protected ICoreDatabaseContext DatabaseContext { get; set; }

        public CoreRepository(ICoreDatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = default)
        {
            var entities = DatabaseContext.Set<TEntity>().AsQueryable();
            if (expression != default)
            {
                entities = entities.Where(expression);
            }
            return entities;
        }

        public TEntity Create(TEntity entity)
        {
            var entityEntry = DatabaseContext.Set<TEntity>().Add(entity);
            return entityEntry.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            if (entity.Id != default)
            {
                var entityEntry = DatabaseContext.Set<TEntity>().Update(entity);
                return entityEntry.Entity;
            }

            return null;
        }

        public TEntity Delete(long id)
        {
            var entities = Get(x => x.Id == id);
            if (entities.Any())
            {
                var entity = entities.First();
                var entityEntry = DatabaseContext.Set<TEntity>().Remove(entity);
                return entityEntry.Entity;
            }

            return null;
        }
    }
}
