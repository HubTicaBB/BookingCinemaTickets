using Microsoft.AspNetCore.Identity;
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
        public ShoppingCart()
        {
            Count = 1;
        }

        public int Id { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public IdentityUser ApplicationUser { get; set; }

        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public Showtime Item { get; set; }
        
        [Range(1, 100)]
        public int Count { get; set; }

        [NotMapped]
        public decimal Price { get; set; }
    }
}
