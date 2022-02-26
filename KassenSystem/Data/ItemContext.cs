using KassenSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace KassenSystem.Data
{
    public class ItemContext : DbContext
    {
        private const int maxRegister = 5;
        public ItemContext(DbContextOptions<ItemContext> options) : base(options)
        {
        }
        
        public DbSet<CheckoutItem> CheckoutItemModels0 { get; set; }
        public DbSet<Sale> SaleModels0 { get; set; }
        public DbSet<Item> ItemModels { get; set; }
        public DbSet<Register> RegisterModels { get; set; }
        /*
        public DbSet<CheckoutItem> CheckoutItemModels1 { get; set; }
        public DbSet<Sale> SaleModels1 { get; set; }
        public DbSet<CheckoutItem> CheckoutItemModels2 { get; set; }
        public DbSet<Sale> SaleModels2 { get; set; }
        public DbSet<CheckoutItem> CheckoutItemModels3 { get; set; }
        public DbSet<Sale> SaleModels3 { get; set; }
        public DbSet<CheckoutItem> CheckoutItemModels4 { get; set; }
        public DbSet<Sale> SaleModels4 { get; set; }
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().ToTable("Item");

           
            modelBuilder.Entity<CheckoutItem>().ToTable("CheckoutItem");

            modelBuilder.Entity<Sale>().ToTable("Sale");
            modelBuilder.Entity<Register>().ToTable("Register");
           
            
        }
    }
}