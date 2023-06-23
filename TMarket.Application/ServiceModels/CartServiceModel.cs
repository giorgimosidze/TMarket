using System.Collections.Generic;

namespace TMarket.Application.ServiceModels
{
   public class CartServiceModel
    {
        public int Id { get; set; }
        public ICollection<CartProductServiceModel> CartProducts { get; set; }
        public int UserId { get; set; }
    }
}
