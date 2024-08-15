using application.Model.Dto.PersonalInfo;
using FluentValidation;

namespace application.Validators.PersonalInfo
{
    public class PersonalInfoDataValidator : AbstractValidator<AddPersonalInfoRequestDto>
    {
        public PersonalInfoDataValidator()
        {
            RuleFor(x => x.FirstName)
                        .NotNull().WithMessage("First Name cannot be null.")
                        .NotEmpty().WithMessage("First Name cannot be empty.");

            RuleFor(x => x.MiddleName)
                        .NotNull().WithMessage("Middle Name cannot be null.");

            RuleFor(x => x.LastName)
                        .NotNull().WithMessage("Last Name cannot be null.")
                        .NotEmpty().WithMessage("Last Name cannot be empty.");

            RuleFor(x => x.Address)
                        .NotNull().WithMessage("Address cannot be null.")
                        .NotEmpty().WithMessage("Address cannot be empty.");

            RuleFor(x => x.Status).NotNull()
                        .NotEmpty()
                        .Must(o => _status.Contains(o))
                        .WithMessage("Status must be one of: Single, Married, Widowed, NA");

            RuleFor(x => x.Age)
                      .NotNull().WithMessage("Age cannot be null.")
                      .NotEmpty().WithMessage("Age cannot be empty.")
                      .GreaterThan(0).WithMessage("Age must be a positive number.")
                      .LessThanOrEqualTo(120).WithMessage("Age must be less than or equal to 120.");
        }

        readonly List<string> _status = new List<string>() {"Single", "Married", "Widowed", "NA" };
    }
}
