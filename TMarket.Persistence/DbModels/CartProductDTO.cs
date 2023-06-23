using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TMarket.Persistence.DbModels.Interfaces;

namespace TMarket.Persistence.DbModels
{
   public class CartProductDTO : IDbEntity
    {
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }

        public int CartId { get; set; }
        public CartDTO Cart { get; set; }

        [NotMapped]
        public bool IsDeleted { get; set; }
        [NotMapped]
        public DateTime InsertDate { get; set; }
        [NotMapped]
        public DateTime UpdateDate { get; set; }
        [NotMapped]
        public DateTime? DeleteDate { get; set; }
        
    }
}
