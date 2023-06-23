using TMarket.Persistence.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TMarket.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductDTO>
    {
        public void Configure(EntityTypeBuilder<ProductDTO> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired(true)
                .HasMaxLength(30);

            builder.Property(p => p.Price)
                .IsRequired(true)
                .HasColumnType("decimal(18,4)");

            builder.Property(p => p.UsefulnessTerm)
                .IsRequired(true)
                .HasColumnType("date");
        }
    }
}
