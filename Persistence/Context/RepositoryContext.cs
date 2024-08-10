using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Persistence.Context
{
    public class RepositoryContext : DbContext
    {
        public DbSet<Store> Stores { get; private set; }
        public DbSet<Product> Products { get; private set; }
        public DbSet<StockItem> StockItems { get; private set; }
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Store>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Stores)
                .UsingEntity<StockItem>();

            modelBuilder.Entity<StockItem>()
                .HasIndex(st => new { st.StoreId, st.ProductId })
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
