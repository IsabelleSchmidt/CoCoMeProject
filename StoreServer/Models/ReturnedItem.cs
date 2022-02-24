namespace StoreServer.Models
{
    public class ReturnedItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public ReturnedItem(int id, string name, int count)
        {
            Id = id;
            Name = name;
            Count = count;
        }
    }
}
