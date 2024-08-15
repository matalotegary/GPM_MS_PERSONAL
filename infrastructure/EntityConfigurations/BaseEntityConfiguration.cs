using common.library.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.EntityConfigurations
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(o => o.CreatedBy).IsRequired().HasMaxLength(256);
            builder.Property(o => o.CreatedByType).IsRequired().HasMaxLength(24);
            builder.Property(o => o.CreatedOn).IsRequired();
            builder.Property(o => o.ModifiedBy).IsRequired().HasMaxLength(256);
            builder.Property(o => o.ModifiedByType).IsRequired().HasMaxLength(24);
            builder.Property(o => o.ModifiedOn).IsRequired();
            builder.Property(o => o.IsActive).IsRequired();
        }
    }
}
