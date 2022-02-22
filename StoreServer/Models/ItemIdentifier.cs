namespace StoreServer.Models
{
    public class ItemIdentifier
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public InventoryItem InventoryItem { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
