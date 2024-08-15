using domain.DomainModels.DefaultUser;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.EntityConfigurations.DefaultUser
{
    public class CodeDefaultUserEntityConfiguration : BaseEntityConfiguration<CodeDefaultUserEntity>
    {
        public override void Configure(EntityTypeBuilder<CodeDefaultUserEntity> builder)
        {
            base.Configure(builder);

            builder.HasKey(o => o.ID);

            builder.Property(o => o.ID)
                   .IsRequired(true);

            builder.Property(o => o.UserName)
               .IsRequired(true)
               .HasMaxLength(100);

            builder.Property(o => o.Password)
                   .IsRequired(true)
                   .HasMaxLength(100);
        }
    }
}
