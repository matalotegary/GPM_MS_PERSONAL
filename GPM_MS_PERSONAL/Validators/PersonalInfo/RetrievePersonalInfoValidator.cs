using application.Model.Dto.PersonalInfo;
using FluentValidation;

namespace application.Validators.PersonalInfo
{
    public class RetrievePersonalInfoValidator : AbstractValidator<RetrievePersonalInfoRequestDto>
    {
        public RetrievePersonalInfoValidator()
        {
            RuleFor(x => x.TransactionNumberRequestID)
                .NotNull().NotEmpty()
                .WithMessage("TransactionNumberRequestID cannot be null or empty");  
        }
    }
}
