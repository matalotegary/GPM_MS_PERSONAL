using domain.DomainModels.PersonalInfo;
using infrastructure.EntityConfigurations.PersonalInfo;
using Microsoft.EntityFrameworkCore;

namespace infrastructure
{
    public class PersonalInfoDbContext : DbContext
    {

        public PersonalInfoDbContext(DbContextOptions<PersonalInfoDbContext> options)
         : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Personal Info
            modelBuilder.ApplyConfiguration(new PersonalInfoEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        //ef core will ensure non-null value here, thus can use null-forgiving operator here

        public DbSet<PersonalInfoEntity> PersonalInfo { get; protected set; } = null!;
    }
}
