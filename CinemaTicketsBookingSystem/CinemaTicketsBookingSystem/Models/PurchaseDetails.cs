using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketsBookingSystem.Models
{
    public class PurchaseDetails
    {
        public int Id { get; set; }

        [Required]
        public int PurchaseId { get; set; }
        [ForeignKey("PurchaseId")]
        public PurchaseHeader PurchaseHeader { get; set; }

        [Required]
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Showtime Item { get; set; }

        public int Count { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
    }
}
