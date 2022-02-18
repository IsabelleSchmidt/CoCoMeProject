﻿using System;
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
    }
}
