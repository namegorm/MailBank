using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AutoMapper;

using Core.Application.ApplicationServices.Interfaces;
using Core.Application.ViewModels.Interfaces;
using Core.Domain.Entities.Interfaces;
using Core.Domain.Repositories.Interfaces;
using Core.Domain.UnitOfWork.Interfaces;

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

        public CoreApplicationService(TRepository linkedRepository, IMapper mapper, ICoreUnitOfWork unitOfWork)
        {
            LinkedRepository = linkedRepository;
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            var entities = LinkedRepository.Get(expression).ToList();
            return entities;
        }

        public virtual async Task<TEntity> CreateAsync(TViewModel viewModel)
        {
            var entity = Mapper.Map<TViewModel, TEntity>(viewModel);
            entity = LinkedRepository.Create(entity);
            var commitAsyncResult = await UnitOfWork.CommitAsync();
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TViewModel viewModel)
        {
            var entity = Mapper.Map<TViewModel, TEntity>(viewModel);
            entity = LinkedRepository.Update(entity);
            var commitAsyncResult = await UnitOfWork.CommitAsync();
            return entity;
        }

        public virtual async Task<TEntity> DeleteAsync(long id)
        {
            var entity = LinkedRepository.Delete(id);
            var commitAsyncResult = await UnitOfWork.CommitAsync();
            return entity;
        }
    }
}
