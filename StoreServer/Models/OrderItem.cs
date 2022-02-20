namespace StoreServer.Models
{
    public class OrderItem
    {
        public int ID { get; set; }

        public int ItemIdentifierID { get; set; }

        public int Count { get; set; }

        public bool Submitted { get; set; }
    }
}
