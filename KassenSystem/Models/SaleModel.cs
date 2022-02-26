using System;
using System.ComponentModel.DataAnnotations;

namespace KassenSystem.Models
{
    public enum PayWay
    {
        Card=0, Cash=1

    }
   
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public PayWay PayWay { get; set; }
        [Required]
        public DateTime PayTime { get; set; }
    }
}
