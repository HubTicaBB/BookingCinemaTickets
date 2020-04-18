using Castle.MicroKernel.SubSystems.Conversion;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketsBookingSystem.Models
{
    public class PurchaseHeader
    {
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public IdentityUser ApplicationUser { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        public string PaymentStatus { get; set; }

        [Required]
        public string UserName { get; set; }

        [NotMapped]
        public ShoppingCart ShoppingCart { get; set; }

        [NotMapped]
        public ICollection<Ticket> Tickets { get; set; }
    }
}
