using System;
using System.Collections.Generic;
using System.Text;
using TMarket.Persistence.DbModels.Interfaces;

namespace TMarket.Persistence.DbModels
{
  public  class CartDTO : IDbEntity
    {
        public CartDTO()
        {
            CartProducts = new HashSet<CartProductDTO>();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string JobId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public ICollection<CartProductDTO> CartProducts { get; set; }
        public UserDTO User { get; set; }
    }
}
