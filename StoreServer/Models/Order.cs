using System.ComponentModel.DataAnnotations;

namespace StoreServer.Models
{
    public class Order
    {
        public int ID { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime RecieveDate { get; set; }

        public ICollection<OrderItem> OrderedItems { get; set; }
    }
}
