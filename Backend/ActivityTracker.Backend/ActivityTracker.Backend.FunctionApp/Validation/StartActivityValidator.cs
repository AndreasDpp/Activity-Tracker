using ActivityTracker.Backend.FunctionApp.Models;
using FluentValidation;

namespace ActivityTracker.Backend.FunctionApp.Validation
{
    public class StartActivityValidator : AbstractValidator<StartActivityModel>
    {
        public StartActivityValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description must not be empty")
                .MaximumLength(200).WithMessage("Description maximum length is 200 characters");

            RuleFor(x => x.Time).NotNull().Must(BeAValidDate).WithMessage("Start date is required.")
                .Must(DateMustNotBeInFuture).WithMessage("Start date can't be in the future.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }

        private bool DateMustNotBeInFuture(DateTime date)
        {
            return date < DateTime.Now;
        }
    }
}
