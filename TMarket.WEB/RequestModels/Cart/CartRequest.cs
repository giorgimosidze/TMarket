using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMarket.WEB.RequestModels.Cart
{
    public class CartRequest
    {
        [Display(Name = "პროდუქტის ლისტ")]
        public ICollection<ProductCartRequset> CartProducts { get; set; }
        [Display(Name = "იუზერის აიდ")]
        public int UserId { get; set; }
    }
}
