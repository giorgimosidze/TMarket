using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMarket.Persistence.DbModels;

namespace TMarket.Persistence.Configurations
{
    public class CartProductConfiguration : IEntityTypeConfiguration<CartProductDTO>
    {
        public void Configure(EntityTypeBuilder<CartProductDTO> builder)
        {
            builder.HasKey(op => new { op.ProductId, op.CartId });
        }
    }
}
