using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMarket.Persistence.DbModels;

namespace TMarket.Persistence.Configurations
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProductDTO>
    {
        public void Configure(EntityTypeBuilder<OrderProductDTO> builder)
        {
            builder.HasKey(op => new {op.ProductId, op.OrderId});
        }
    }
}