using System.Collections.Generic;
using System.Linq;
using FluentValidation.Validators;
using TMarket.WEB.RequestModels.Orders;

namespace TMarket.WEB.Validators
{
    public class UniqueInnerCollectionValidator : PropertyValidator
    {
        public UniqueInnerCollectionValidator() 
            : base("პროდუქტი აიდ(ებ)ით: {symbols} უკვე არსებობს შეკვეთაში!") { }

        protected override bool IsValid(PropertyValidatorContext context) {
            var listOfProductOrder = context.PropertyValue as List<ProductOrderRequest>;
            
            if (listOfProductOrder == null)
                return true;

            var nonUniqueKeys = listOfProductOrder.GroupBy(x => x.ProductId)
                .Where(x => x.Count() > 1).Select(x => x.Key).ToList();

            if (nonUniqueKeys.Count > 0) 
            {
                string failedItems = string.Join(", ", nonUniqueKeys.ToArray());
                context.MessageFormatter.AppendArgument("symbols", failedItems);
                return false;
            }

            return true;
        }
    }
}