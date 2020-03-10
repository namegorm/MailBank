using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Core.Application.ApplicationServices.Interfaces;
using Core.Application.Models;
using Core.Application.ViewModels.Interfaces;
using Core.Domain.Entities.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        protected CoreApiController(TApplicationService applicationService)
        {
            ApplicationService = applicationService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAsync()
        {
            var entities = await ApplicationService.GetAsync();
            return Ok(CoreResultModel.Create(HttpStatusCode.OK, data: entities));
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAsync(long id)
        {
            var entities = await ApplicationService.GetAsync(x => x.Id == id);
            return Ok(CoreResultModel.Create(HttpStatusCode.OK, data: entities.FirstOrDefault()));
        }

        [HttpPost]
        public virtual async Task<IActionResult> PostAsync(TViewModel viewModel)
        {
            var entity = await ApplicationService.CreateAsync(viewModel);
            return Ok(CoreResultModel.Create(HttpStatusCode.Created, data: entity));
        }

        [HttpPut]
        public virtual async Task<IActionResult> PutAsync(TViewModel viewModel)
        {
            var entity = await ApplicationService.UpdateAsync(viewModel);
            return Ok(CoreResultModel.Create(HttpStatusCode.OK, data: entity));
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(long id)
        {
            var entity = await ApplicationService.DeleteAsync(id);
            return Ok(CoreResultModel.Create(HttpStatusCode.OK, data: entity));
        }
    }
}
