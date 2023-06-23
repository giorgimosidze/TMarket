using System;
using System.Collections.Generic;
using TMarket.Persistence.DbModels.Interfaces;

namespace TMarket.Persistence.DbModels
{
    public class CategoryDTO : IDbEntity
    {
        public CategoryDTO()
        {
            Products = new HashSet<ProductDTO>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime? DeleteDate { get; set; }

        public ICollection<ProductDTO> Products { get; set; }
    }
}
