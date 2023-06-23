using TMarket.Persistence.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TMarket.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserDTO>
    {
        public void Configure(EntityTypeBuilder<UserDTO> builder)
        {
            builder.Property(u => u.Name)
                .IsRequired(true)
                .HasMaxLength(20);

            builder.Property(u => u.Lastname)
                .IsRequired(true)
                .HasMaxLength(35);
        }
    }
}
