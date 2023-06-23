using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TMarket.WEB.RequestModels.Orders
{
    public class OrderRequest
    {
        [Display(Name="პროდუქტის ლისტ")]
        public ICollection<ProductOrderRequest> OrderProducts { get; set; }
        [Display(Name="იუზერის აიდ")]
        public int UserId { get; set; }
    }
}
