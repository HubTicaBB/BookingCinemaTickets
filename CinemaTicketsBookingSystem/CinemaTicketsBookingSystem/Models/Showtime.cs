using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketsBookingSystem.Models
{
    public class Showtime
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }

        [Required]
        public int MovieId { get; set; }
        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
        
        [Required]
        public int CinemaHallId { get; set; }
        [ForeignKey("CinemaHallId")]
        public CinemaHall CinemaHall { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal TicketPrice { get; set; }

        public int TicketsAvailable { get; set; }
    }
}
