using FluentValidation;
using TMarket.WEB.Commands.UserCommands;
using TMarket.WEB.Helpers.Constants;

namespace TMarket.WEB.Validators
{
    public class UserValidator : AbstractValidator<UserRequestCommand>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ModelConstants.PropertyNotFound)
                .OverridePropertyName("სახელ")
                .Length(4, 20).WithMessage(ModelConstants.StringLengthError)
                .Matches(ModelConstants.NameRegEx).WithMessage(ModelConstants.InvalidName);

            RuleFor(p => p.Lastname)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ModelConstants.PropertyNotFound)
                .OverridePropertyName("გვარ")
                .Length(5, 35).WithMessage(ModelConstants.StringLengthError)
                .Matches(ModelConstants.NameRegEx).WithMessage(ModelConstants.InvalidName);
        }
    }
}
