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

        [Range(1, 12, ErrorMessage = "Number of tickets per purchase: min 1 - max 12")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter a valid number. The minimum number of tickets is 1.")]        
        public int Count { get; set; }
    }
}
