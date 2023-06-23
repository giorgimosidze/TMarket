using System;
using System.Collections.Generic;
using TMarket.Persistence.DbModels.Interfaces;

namespace TMarket.Persistence.DbModels
{
    public class OrderDTO : IDbEntity
    {
        public OrderDTO()
        {
            OrderProducts = new HashSet<OrderProductDTO>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public ICollection<OrderProductDTO> OrderProducts { get; set; }
        public UserDTO User { get; set; }
    }
}
