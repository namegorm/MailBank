using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AutoMapper;

using Core.Application.ApplicationServices.Interfaces;
using Core.Application.ViewModels.Interfaces;
using Core.Domain.Entities.Interfaces;
using Core.Domain.Repositories.Interfaces;
using Core.Domain.UnitOfWork.Interfaces;

using Microsoft.Extensions.Logging;

namespace Core.Application.ApplicationServices.Implementations
{
    public class CoreApplicationService<TEntity, TViewModel, TRepository> : ICoreApplicationService<TEntity, TViewModel>
        where TEntity : class, ICoreEntity, new()
        where TViewModel : ICoreViewModel
        where TRepository : ICoreRepository<TEntity>
    {
        protected TRepository LinkedRepository { get; }
        protected IMapper Mapper { get; }
        protected ICoreUnitOfWork UnitOfWork { get; }
        private readonly ILogger<CoreApplicationService<TEntity, TViewModel, TRepository>> _logger;

        public CoreApplicationService(TRepository linkedRepository, IMapper mapper, ICoreUnitOfWork unitOfWork, ILogger<CoreApplicationService<TEntity, TViewModel, TRepository>> logger)
        {
            LinkedRepository = linkedRepository;
            Mapper = mapper;
            UnitOfWork = unitOfWork;
            _logger = logger;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            using (_logger.BeginScope($"{nameof(CoreApplicationService<TEntity, TViewModel, TRepository>)}.{nameof(CoreApplicationService<TEntity, TViewModel, TRepository>.GetAsync)}"))
            {
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    _logger.LogInformation("Expression: {@Expression}", expression);
                    var entities = LinkedRepository.Get(expression).ToList();
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

        public virtual async Task<TEntity> CreateAsync(TViewModel viewModel)
        {
            using (_logger.BeginScope($"{nameof(CoreApplicationService<TEntity, TViewModel, TRepository>)}.{nameof(CoreApplicationService<TEntity, TViewModel, TRepository>.CreateAsync)}"))
            {
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    var entity = Mapper.Map<TViewModel, TEntity>(viewModel);
                    entity = LinkedRepository.Create(entity);
                    var commitAsyncResult = await UnitOfWork.CommitAsync();
                    _logger.LogInformation("View model: {@ViewModel}, entity: {@Entity}", viewModel, entity);
                    return entity;
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

        public virtual async Task<TEntity> UpdateAsync(TViewModel viewModel)
        {
            using (_logger.BeginScope($"{nameof(CoreApplicationService<TEntity, TViewModel, TRepository>)}.{nameof(CoreApplicationService<TEntity, TViewModel, TRepository>.UpdateAsync)}"))
            {
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    var entity = Mapper.Map<TViewModel, TEntity>(viewModel);
                    entity = LinkedRepository.Update(entity);
                    var commitAsyncResult = await UnitOfWork.CommitAsync();
                    _logger.LogInformation("View model: {@ViewModel}, entity: {@Entity}", viewModel, entity);
                    return entity;
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

        public virtual async Task<TEntity> DeleteAsync(long id)
        {
            using (_logger.BeginScope($"{nameof(CoreApplicationService<TEntity, TViewModel, TRepository>)}.{nameof(CoreApplicationService<TEntity, TViewModel, TRepository>.DeleteAsync)}"))
            {
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    var entity = LinkedRepository.Delete(id);
                    var commitAsyncResult = await UnitOfWork.CommitAsync();
                    _logger.LogInformation("Id: {Id}, entity: {@Entity}", id, entity);
                    return entity;
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
