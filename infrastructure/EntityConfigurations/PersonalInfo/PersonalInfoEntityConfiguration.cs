using domain.DomainModels.PersonalInfo;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.EntityConfigurations.PersonalInfo
{
    public class PersonalInfoEntityConfiguration : BaseEntityConfiguration<PersonalInfoEntity>
    {
        public override void Configure(EntityTypeBuilder<PersonalInfoEntity> builder)
        {
            base.Configure(builder);

            builder.HasKey(o => o.ID);

            builder.Property(o => o.ID)
                .IsRequired(true);

            builder.Property(o => o.FirstName)
               .IsRequired(true)
               .HasMaxLength(100);

            builder.Property(o => o.MiddleName)
                .IsRequired(true)
                .HasMaxLength(100);

            //override more props here 
        }
    }
}
