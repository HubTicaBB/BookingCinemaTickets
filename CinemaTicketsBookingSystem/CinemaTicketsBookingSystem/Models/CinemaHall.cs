using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaTicketsBookingSystem.Models
{
    public class CinemaHall
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int SeatingCapacity { get; set; }

        [NotMapped]
        public ICollection<Seat> Seats { get; set; }
    }
}