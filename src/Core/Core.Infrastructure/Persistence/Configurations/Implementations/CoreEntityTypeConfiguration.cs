using Core.Domain.Entities.Interfaces;
using Core.Infrastructure.Persistence.Configurations.Interfaces;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastructure.Persistence.Configurations.Implementations
{
    public abstract class CoreEntityTypeConfiguration<TEntity> : ICoreEntityTypeConfiguration<TEntity>
        where TEntity : class, ICoreEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}
