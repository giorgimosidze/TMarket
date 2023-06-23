using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TMarket.WEB.RequestModels.Cart
{
    public class CartResponse
    {
        public int Id { get; set; }
        [Display(Name = "პროდუქტის აიდ")]
        public ICollection<ProductCartResponse> CartProducts { get; set; }
        [Display(Name = "იუზერის აიდ")]
        public int UserId { get; set; }
        public decimal OrderTotalPrice
        {
            get => CartProducts.Sum(x => x.TotalPrice);
        }
    }
}
