using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMarket.Persistence.DbModels;

namespace TMarket.Persistence.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<CartDTO>
    {
        public void Configure(EntityTypeBuilder<CartDTO> builder)
        {
            builder.Property(c => c.JobId).HasMaxLength(10);
        }
    }
}
