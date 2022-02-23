using KassenSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace KassenSystem.Data
{
    public class ItemContext : DbContext
    {
        public ItemContext(DbContextOptions<ItemContext> options) : base(options)
        {
        }
        
        public DbSet<ItemModel> ItemModels { get; set; }
        public DbSet<CheckoutItemModel> CheckoutItemModels { get; set; }
        public DbSet<PaymentModel> PaymentModels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //hier wird prob. nur 1 Datenbank erstellt
            modelBuilder.Entity<CheckoutItemModel>().ToTable("CheckoutItem");
            modelBuilder.Entity<ItemModel>().ToTable("Item");

            modelBuilder.Entity<PaymentModel>().ToTable("Payment");
        }
    }
}