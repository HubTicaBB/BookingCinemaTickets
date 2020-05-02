using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketsBookingSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name on Card")]
        public string NameOnCard { get; set; }

        [Required]
        [Display(Name = "Credit Card Number")]
        [RegularExpression(@"^([0-9]{16})$", ErrorMessage = "Invalid Credit Card Number")]
        public string CardNumber { get; set; }

        [Required]
        [Display(Name = "Expiration Date")]
        public string ExpirationDate { get; set; }

        [Required]
        [Display(Name = "Card Verification Number (CVV)")]
        [RegularExpression(@"^([0-9]{3})$", ErrorMessage = "Invalid Card Verification Number")]
        public string CVV { get; set; }
    }
}
