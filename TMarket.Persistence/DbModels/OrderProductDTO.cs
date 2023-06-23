using System;
using System.ComponentModel.DataAnnotations.Schema;
using TMarket.Persistence.DbModels.Interfaces;

namespace TMarket.Persistence.DbModels
{
    public class OrderProductDTO : IDbEntity
    {
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public int Quantity { get; set; }

        public int OrderId { get; set; }
        public OrderDTO Order { get; set; }

        [NotMapped]
        public bool IsDeleted { get => ((IDbEntity)Order).IsDeleted; set => ((IDbEntity)Order).IsDeleted = value; }
        [NotMapped]
        public DateTime InsertDate { get => ((IDbEntity)Order).InsertDate; set => ((IDbEntity)Order).InsertDate = value; }
        [NotMapped]
        public DateTime UpdateDate { get => ((IDbEntity)Order).UpdateDate; set => ((IDbEntity)Order).UpdateDate = value; }
        [NotMapped]
        public DateTime? DeleteDate { get => ((IDbEntity)Order).DeleteDate; set => ((IDbEntity)Order).DeleteDate = value; }
    }
}