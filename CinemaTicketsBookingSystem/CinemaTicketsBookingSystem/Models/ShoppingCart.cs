using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketsBookingSystem.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public int ShowtimeId { get; set; }
        [ForeignKey("ShowtimeId")]
        public Showtime Showtime { get; set; }

        [Range(1, Int32.MaxValue)]
        public int Count { get; set; }
    }
}
