using System;
using System.ComponentModel.DataAnnotations;

namespace KassenSystemAngular.Models
{
    public enum PayWay
    {
        Card, Cash
    }
    public class PaymentModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public PayWay PayWay { get; set; }
        [Required]
        public DateTime PayTime { get; set; }
    }
}
