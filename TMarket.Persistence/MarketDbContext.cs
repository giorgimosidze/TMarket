using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TMarket.Persistence.DbModels;
using TMarket.Persistence.DbModels.Interfaces;

namespace TMarket.Persistence
{
    public class MarketDbContext : DbContext
    {
        public DbSet<ProductDTO> Products { get; set; }
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<OrderDTO> Orders { get; set; }
        public DbSet<OrderProductDTO> OrderProducts { get; set; }
        public DbSet<CategoryDTO> Categories { get; set; }

        public MarketDbContext(DbContextOptions<MarketDbContext> options) :
            base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(MarketDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is IDbEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((IDbEntity)entityEntry.Entity).UpdateDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((IDbEntity)entityEntry.Entity).InsertDate = DateTime.Now;
                }

                if (((IDbEntity)entityEntry.Entity).IsDeleted)
                {
                    ((IDbEntity)entityEntry.Entity).DeleteDate = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
