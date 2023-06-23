using System.Collections.Generic;

namespace TMarket.Application.ServiceModels
{
    public class OrderServiceModel
    {
        public int Id { get; set; }
        public ICollection<OrderProductServiceModel> OrderProducts { get; set; }
        public int UserId { get; set; }
    }
}