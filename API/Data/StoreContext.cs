using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using API.Entities.PRAggregate;
using API.Entities.AssetAggregate;

namespace API.Data
{
    public class StoreContext : IdentityDbContext<User, Role, int>
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<PurchaseRequisition> PurchaseRequisitions { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<TaxInvoice> TaxInvoices { get; set; }
        public DbSet<TaxItem> TaxItems { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<AssetDetails> AssetDetails { get; set; }
        public DbSet <Owner> Owners { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Asset>().HasKey(a => a.Id);
            builder.Entity<Stock>().HasKey(s => s.Id);

            builder.Entity<Role>()
                .HasData(
                    new Role { Id = 1, Name = "Emp", NormalizedName = "EMP" },
                    new Role { Id = 2, Name = "Approver", NormalizedName = "APPROVER" },
                    new Role { Id = 3, Name = "Admin", NormalizedName = "ADMIN" },
                    new Role { Id = 4, Name = "Manager", NormalizedName = "MANAGER" },
                    new Role { Id = 5, Name = "Purchasing", NormalizedName = "PURCHASING" },
                    new Role { Id = 6, Name = "Asset", NormalizedName = "ASSET" }
                );
        }

        public override int SaveChanges()
        {
            // Find newly added entities of type Asset
            var newAssets = ChangeTracker.Entries<Asset>()
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity);

            foreach (var newAsset in newAssets)
            {
                // Find the corresponding Stock entity
                var stock = Set<Stock>().Find(newAsset.StockId);

                if (stock != null)
                {
                    // Update the stock count
                    stock.Total += 1;
                }
            }

            return base.SaveChanges();
        }

    }
}