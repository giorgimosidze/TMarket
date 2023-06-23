using System;
using FluentValidation;
using TMarket.WEB.Helpers.Constants;
using TMarket.WEB.RequestModels.Products;

namespace TMarket.WEB.Validators
{
    public class ProductValidator : AbstractValidator<ProductRequest>
    {
        public ProductValidator()
        {
            RuleFor(p => p.AvailableCount)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ModelConstants.PropertyNotFound)
                .OverridePropertyName("არსებული რაოდენობ")
                .GreaterThanOrEqualTo(0).WithMessage(ModelConstants.MustBeMoreThanZero);

            RuleFor(p => p.CategoryId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ModelConstants.PropertyNotFound)
                .OverridePropertyName("კატეგორიის აიდ")
                .GreaterThanOrEqualTo(0).WithMessage(ModelConstants.MustBeMoreThanZero);

            RuleFor(p => p.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ModelConstants.PropertyNotFound)
                .OverridePropertyName("სახელ")
                .Length(3, 30).WithMessage(ModelConstants.StringLengthError);

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage(ModelConstants.PropertyNotFound)
                .OverridePropertyName("ფას");

            RuleFor(p => p.IsAvailable)
                .Equal(false).When(p => p.AvailableCount == 0)
                .WithMessage(ModelConstants.IsAvailableLogicError)
                .OverridePropertyName("ხელმისაწვდომობ");

            RuleFor(p => p.UsefulnessTerm)
                .NotEmpty().WithMessage(ModelConstants.PropertyNotFound)
                .OverridePropertyName("ვად");

            RuleFor(p => p.UsefulnessTerm)
                .GreaterThan(DateTime.Now)
                .WithMessage("ვადა აუცილებლად მომავალში უნდა იყოს!");
        }
    }
}
