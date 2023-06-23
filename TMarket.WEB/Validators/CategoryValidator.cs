using FluentValidation;
using TMarket.WEB.Commands.CategoryCommands;
using TMarket.WEB.Helpers.Constants;

namespace TMarket.WEB.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryRequestCommand>
    {
        public CategoryValidator()
        {
            RuleFor(p => p.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ModelConstants.PropertyNotFound)
                .OverridePropertyName("სახელ")
                .Length(2, 20).WithMessage(ModelConstants.StringLengthError)
                .Matches(ModelConstants.NameRegEx).WithMessage(ModelConstants.InvalidName);
        }
    }
}
