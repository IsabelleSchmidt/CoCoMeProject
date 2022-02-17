﻿namespace StoreServer.Models
{
    public class InventoryItem
    {
        public int ID { get; set; }
        public int ItemIdentifierID { get; set; }

        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}

