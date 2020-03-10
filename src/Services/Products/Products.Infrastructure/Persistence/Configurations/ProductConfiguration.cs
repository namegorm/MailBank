
using Core.Infrastructure.Persistence.Configurations.Implementations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Products.Domain.Entities;

namespace Products.Infrastructure.Persistence.Configurations
{
    class ProductConfiguration : CoreEntityTypeConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(500);
        }
    }
}
