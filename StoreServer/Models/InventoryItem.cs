using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreServer.Models
{
    [Table("InventoryItem")]
    public class InventoryItem
    {
        [Key]
        public int ID { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public ItemIdentifier ItemIdentifier { get; set; }
    }
}

