using Core.Domain.Entities.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistence.Configurations.Interfaces
{
    public interface ICoreEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, ICoreEntity
    {
    }
}
