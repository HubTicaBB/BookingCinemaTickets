using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketsBookingSystem.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalAmount { get; set; }

        [NotMapped]
        public ShoppingCart ShoppingCart { get; set; }

        [NotMapped]
        public ICollection<Ticket> Tickets { get; set; }
    }
}
