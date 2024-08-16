using domain.DomainModels.PersonalInfo;
using domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace infrastructure.Repositories.PersonalInfo
{
    public class PersonalInfoRepository : IPersonalInfoRepository
    {
        private readonly PersonalInfoDbContext _dbContext;

        public PersonalInfoRepository(PersonalInfoDbContext personalInfoDbContext)
        {
            _dbContext = personalInfoDbContext ?? throw new ArgumentNullException(nameof(personalInfoDbContext));
        }
        public async Task<PersonalInfoEntity?> FindByTransactionNumberRequestIDAsync(Guid transactionNumberRequestID)
        {
            return await _dbContext.PersonalInfo.FirstOrDefaultAsync(f => f.TransactionNumberRequestID.Equals(transactionNumberRequestID));
        }
        public async Task AddPersonalInfoAsync(PersonalInfoEntity entity)
        {
            await _dbContext.PersonalInfo.AddAsync(entity);
        }
        public void UpdatePersonalInfoAsync(PersonalInfoEntity entity)
        {
            _dbContext.PersonalInfo.Update(entity);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
        public IExecutionStrategy GetExecutionStrategy()
        {
            return _dbContext.Database.CreateExecutionStrategy();
        }
    }
}
