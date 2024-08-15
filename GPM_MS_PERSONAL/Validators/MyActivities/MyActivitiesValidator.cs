using application.Model.Dto.MyActivities;
using FluentValidation;

namespace application.Validators.MyActivities
{
    public class MyActivitiesValidator : AbstractValidator<MyActivitiesRequestDto>
    {
        public MyActivitiesValidator()
        {
            RuleFor(x => x.TransactionNumber).NotNull().NotEmpty();
        }
    }
}
