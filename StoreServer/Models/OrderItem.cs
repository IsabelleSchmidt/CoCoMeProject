namespace StoreServer.Models
{
    public class OrderItem
    {
        public int ID { get; set; }

        public int Count { get; set; }

        public bool Submitted { get; set; }
        public int ItemIdentifierForeignKey { get; set; }
        public ItemIdentifier ItemIdentifier { get; set; }
        public int OrderForeignKey { get; set; }
        public Order Order { get; set; }

    }
}
