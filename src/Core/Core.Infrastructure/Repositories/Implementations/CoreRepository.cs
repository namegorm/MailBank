using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

using Core.Domain.Entities.Interfaces;
using Core.Domain.Repositories.Interfaces;
using Core.Infrastructure.Persistence.DatabaseContexts.Interfaces;

using Microsoft.Extensions.Logging;

namespace Core.Infrastructure.Repositories.Implementations
{
    public class CoreRepository<TEntity> : ICoreRepository<TEntity>
        where TEntity : class, ICoreEntity
    {
        protected ICoreDatabaseContext DatabaseContext { get; set; }
        private readonly ILogger<CoreRepository<TEntity>> _logger;

        public CoreRepository(ICoreDatabaseContext databaseContext, ILogger<CoreRepository<TEntity>> logger)
        {
            DatabaseContext = databaseContext;
            _logger = logger;
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> expression = default)
        {
            using (_logger.BeginScope($"{nameof(CoreRepository<TEntity>)}.{nameof(CoreRepository<TEntity>.Get)}"))
            {
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    _logger.LogDebug("Method started.");
                    _logger.LogInformation("Expression: {@Expression}", expression);
                    var entities = DatabaseContext.Set<TEntity>().AsQueryable();
                    if (expression != default)
                    {
                        entities = entities.Where(expression);
                    }
                    return entities;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Method failed.");
                    throw;
                }
                finally
                {
                    _logger.LogDebug("Method finished. Duration: {Duration}", stopwatch.Elapsed);
                }
            }
        }

        public virtual TEntity Create(TEntity entity)
        {
            using (_logger.BeginScope($"{nameof(CoreRepository<TEntity>)}.{nameof(CoreRepository<TEntity>.Create)}"))
            {
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    _logger.LogDebug($"Method started.");
                    var entityEntry = DatabaseContext.Set<TEntity>().Add(entity);
                    _logger.LogInformation("Entity: {@Entity}", entityEntry.Entity);
                    return entityEntry.Entity;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Method failed.");
                    throw;
                }
                finally
                {
                    _logger.LogDebug("Method finished. Duration: {Duration}", stopwatch.Elapsed);
                }
            }
        }

        public virtual TEntity Update(TEntity entity)
        {
            using (_logger.BeginScope($"{nameof(CoreRepository<TEntity>)}.{nameof(CoreRepository<TEntity>.Update)}"))
            {
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    _logger.LogDebug($"Method started.");
                    if (entity.Id != default)
                    {
                        var entityEntry = DatabaseContext.Set<TEntity>().Update(entity);
                        _logger.LogInformation("Entity: {@Entity}", entityEntry.Entity);
                        return entityEntry.Entity;
                    }

                    _logger.LogInformation("Entity: {@Entity}", null);
                    return null;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Method failed.");
                    throw;
                }
                finally
                {
                    _logger.LogDebug("Method finished. Duration: {Duration}", stopwatch.Elapsed);
                }
            }
        }

        public virtual TEntity Delete(long id)
        {
            using (_logger.BeginScope($"{nameof(CoreRepository<TEntity>)}.{nameof(CoreRepository<TEntity>.Delete)}"))
            {
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    var entities = Get(x => x.Id == id);
                    if (entities.Any())
                    {
                        var entity = entities.First();
                        var entityEntry = DatabaseContext.Set<TEntity>().Remove(entity);
                        _logger.LogInformation("Entity: {@Entity}", entityEntry.Entity);
                        return entityEntry.Entity;
                    }

                    _logger.LogInformation("Entity: {@Entity}", null);
                    return null;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Method failed.");
                    throw;
                }
                finally
                {
                    _logger.LogDebug("Method finished. Duration: {Duration}", stopwatch.Elapsed);
                }
            }
        }
    }
}
