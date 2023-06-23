using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TMarket.WEB.RequestModels.Orders
{
    public class ProductOrderRequest
    {
        [Display(Name="პროდუქტის აიდ")]
        public int ProductId { get; set; }
        [Display(Name="პროდუქტის რაოდენობ")]
        public int Quantity { get; set; }
    }
}
