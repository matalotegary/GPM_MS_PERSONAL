using common.library.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace domain.DomainModels.PersonalInfo
{
    [Index(nameof(TransactionNumberRequestID))]
    public class PersonalInfoEntity : BaseEntity
    {
        public Guid TransactionNumberRequestID { get; private set; }

        //[ForeignKey("TransactionNumberRequestID")]
        //public virtual TransactionNumberRequestEntity TransactionNumberRequest { get; set; }

        [MaxLength(100)]
        public string? FirstName { get; private set; }

        [MaxLength(100)]
        public string? MiddleName { get; private set; }

        [MaxLength(100)]
        public string? LastName { get; private set; }

        [MaxLength(200)]
        public string? Address { get; private set; }

        [MaxLength(50)]
        public string? Status { get; private set; }

        public int ? Age { get; private set; }

        public PersonalInfoEntity(Guid transactionNumberRequestID, string? firstName, 
            string? middleName, string? lastName, string? address, string? status,
            int? age, string createdBy, string createdByType) : base(createdBy, createdByType)
        {
            TransactionNumberRequestID = transactionNumberRequestID;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Address = address;
            Status = status;
            Age = age;
        }
    }
}
