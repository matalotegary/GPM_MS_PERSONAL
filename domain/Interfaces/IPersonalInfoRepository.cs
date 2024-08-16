using domain.DomainModels.PersonalInfo;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Interfaces
{
    public interface IPersonalInfoRepository
    {
        Task<PersonalInfoEntity?> FindByTransactionNumberRequestIDAsync(Guid transactionNumberRequestID);
        Task AddPersonalInfoAsync(PersonalInfoEntity entity);
        void UpdatePersonalInfoAsync(PersonalInfoEntity entity);
        IExecutionStrategy GetExecutionStrategy();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
