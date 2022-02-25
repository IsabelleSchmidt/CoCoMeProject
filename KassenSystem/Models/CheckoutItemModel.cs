using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KassenSystem.Models
{
    public class CheckoutItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int PriceSingle { get; set; }
        [Required]
        public int PriceFull { get; set; }
        [Required]
        public int Amount { get; set; }

    }
}
