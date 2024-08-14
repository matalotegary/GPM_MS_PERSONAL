using FluentValidation;
using FluentValidation.Results;

namespace domain.Validators
{
    public static class ValidatorExtension
    {
        public static void Validate<T, TValidator>(this T t)
          where TValidator : AbstractValidator<T>
        {
            var validator = Activator.CreateInstance<TValidator>();
            ValidationResult results = validator.Validate(t);

            if (!results.IsValid)
            {
                throw new ArgumentException(string.Join(Environment.NewLine, results.Errors.Select(e => e.ErrorMessage)));
            }
        }
    }
}
