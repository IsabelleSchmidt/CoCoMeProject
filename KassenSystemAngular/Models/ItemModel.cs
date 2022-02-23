using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KassenSystemAngular.Models
{
    public class ItemModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Amount { get; set; }

    }
}
