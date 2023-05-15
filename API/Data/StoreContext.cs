using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using API.Entities.PRAggregate;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //  builder.Entity<TaxInvoice>()
            //     .HasMany(t => t.ProductIds)
            //     .WithOne(p => p.TaxInvoice)
            //     .HasForeignKey(p => p.TaxInvoiceId);

            // builder.Entity<PurchaseRequisition>()
            //     .HasOne(q => q.Quotation)
            //     .WithOne()
            //     // .HasForeignKey<User>(u => u.UserName)
            //     .HasForeignKey<Quotation>(q => q.Id);

            builder.Entity<Role>()
                .HasData(
                    new Role{Id = 1, Name = "Emp", NormalizedName="EMP"},
                    new Role{Id = 2, Name = "Approver", NormalizedName="APPROVER"},
                    new Role{Id = 3, Name = "Admin", NormalizedName="ADMIN"}
                );
        }
    }
}