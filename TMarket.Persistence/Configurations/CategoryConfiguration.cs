using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMarket.Persistence.DbModels;

namespace TMarket.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryDTO>
    {
        public void Configure(EntityTypeBuilder<CategoryDTO> builder)
        {
            builder.Property(x => x.Name).IsRequired(true).HasMaxLength(30);
        }
    }
}
