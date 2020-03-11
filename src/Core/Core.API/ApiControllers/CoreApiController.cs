using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Core.Application.ApplicationServices.Interfaces;
using Core.Application.Models;
using Core.Application.ViewModels.Interfaces;
using Core.Domain.Entities.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core.API.ApiControllers
{
    [ApiController]
    [Authorize]
    public abstract class CoreApiController<TEntity, TViewModel, TApplicationService> : ControllerBase
        where TEntity : class, ICoreEntity
        where TViewModel : ICoreViewModel
        where TApplicationService : ICoreApplicationService<TEntity, TViewModel>
    {
        protected TApplicationService ApplicationService { get; }
        private readonly ILogger<CoreApiController<TEntity, TViewModel, TApplicationService>> _logger;

        protected CoreApiController(TApplicationService applicationService, ILogger<CoreApiController<TEntity, TViewModel, TApplicationService>> logger)
        {
            ApplicationService = applicationService;
            _logger = logger;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAsync()
        {
            using (_logger.BeginScope($"{nameof(CoreApiController<TEntity, TViewModel, TApplicationService>)}.{nameof(CoreApiController<TEntity, TViewModel, TApplicationService>.GetAsync)}"))
            {
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    var entities = await ApplicationService.GetAsync();
                    return Ok(CoreResultModel.Create(HttpStatusCode.OK, data: entities));
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

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAsync(long id)
        {
            using (_logger.BeginScope($"{nameof(CoreApiController<TEntity, TViewModel, TApplicationService>)}.{nameof(CoreApiController<TEntity, TViewModel, TApplicationService>.GetAsync)}"))
            {
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    var entities = await ApplicationService.GetAsync(x => x.Id == id);
                    _logger.LogInformation("Id: {Id}", id);
                    return Ok(CoreResultModel.Create(HttpStatusCode.OK, data: entities.FirstOrDefault()));
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

        [HttpPost]
        public virtual async Task<IActionResult> PostAsync(TViewModel viewModel)
        {
            using (_logger.BeginScope($"{nameof(CoreApiController<TEntity, TViewModel, TApplicationService>)}.{nameof(CoreApiController<TEntity, TViewModel, TApplicationService>.PostAsync)}"))
            {
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    var entity = await ApplicationService.CreateAsync(viewModel);
                    _logger.LogInformation("View model: {@ViewModel}, entity: {@Entity}", viewModel, entity);
                    return Ok(CoreResultModel.Create(HttpStatusCode.OK, data: entity));
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

        [HttpPut]
        public virtual async Task<IActionResult> PutAsync(TViewModel viewModel)
        {
            using (_logger.BeginScope($"{nameof(CoreApiController<TEntity, TViewModel, TApplicationService>)}.{nameof(CoreApiController<TEntity, TViewModel, TApplicationService>.PutAsync)}"))
            {
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    var entity = await ApplicationService.UpdateAsync(viewModel);
                    _logger.LogInformation("View model: {@ViewModel}, entity: {@Entity}", viewModel, entity);
                    return Ok(CoreResultModel.Create(HttpStatusCode.OK, data: entity));
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

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(long id)
        {
            using (_logger.BeginScope($"{nameof(CoreApiController<TEntity, TViewModel, TApplicationService>)}.{nameof(CoreApiController<TEntity, TViewModel, TApplicationService>.DeleteAsync)}"))
            {
                var stopwatch = Stopwatch.StartNew();
                try
                {
                    var entity = await ApplicationService.DeleteAsync(id);
                    _logger.LogInformation("Id: {Id}, entity: {@Entity}", id, entity);
                    return Ok(CoreResultModel.Create(HttpStatusCode.OK, data: entity));
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
