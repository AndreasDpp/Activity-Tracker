using ActivityTracker.Backend.FunctionApp.Models;
using FluentValidation;

namespace ActivityTracker.Backend.FunctionApp.Validation
{
    public class EndActivityValidator : AbstractValidator<EndActivityModel>
    {
        public EndActivityValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Invalid activity id")
                .MaximumLength(24).WithMessage("Invalid length of the Id")
                .MinimumLength(24).WithMessage("Invalid length of the Id");

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
