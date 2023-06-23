using System;
using System.Collections.Generic;
using TMarket.Persistence.DbModels.Interfaces;

namespace TMarket.Persistence.DbModels
{
    public class ProductDTO : IDbEntity
    {
        public ProductDTO()
        {
            OrderProducts = new HashSet<OrderProductDTO>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public DateTime UsefulnessTerm { get; set; }
        public int AvailableCount { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public ICollection<OrderProductDTO> OrderProducts { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
