using FluentValidation;
using TMarket.WEB.Helpers.Constants;
using TMarket.WEB.RequestModels.Orders;

namespace TMarket.WEB.Validators
{
    public class OrderRequestValidator : AbstractValidator<OrderRequest>
    {
        public OrderRequestValidator()
        {            
            RuleFor(x => x.UserId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty().WithMessage(ModelConstants.PropertyNotFound)
                .OverridePropertyName("იუზერის აიდ")
                .GreaterThanOrEqualTo(0).WithMessage(ModelConstants.MustBeMoreThanZero);

            RuleFor(x => x.OrderProducts)
                .NotEmpty().WithMessage(ModelConstants.PropertyNotFound)
                .OverridePropertyName("პროდუქტებ");

            RuleForEach(x => x.OrderProducts)
                .SetValidator(new ProductOrderRequestValidator());
            
            RuleFor(x => x.OrderProducts).SetValidator(new UniqueInnerCollectionValidator());
        }
    }
}
