using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreServer.Models;

namespace StoreServer.Data
{
    public class StoreServerContext : DbContext
    {
        public StoreServerContext (DbContextOptions<StoreServerContext> options)
            : base(options)
        {
        }

        public DbSet<InventoryItem> InventoryItem { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<ItemIdentifier> ItemIdentifier { get; set; }

        public DbSet<StoreServer.Models.OrderItem> OrderItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryItem>()
                .HasOne(item => item.ItemIdentifier);

            modelBuilder.Entity<OrderItem>()
                .HasOne(item => item.ItemIdentifier);

            modelBuilder.Entity<OrderItem>()
                .HasOne(item => item.Order)
                .WithMany(order => order.OrderItems);
        }
    }
}
