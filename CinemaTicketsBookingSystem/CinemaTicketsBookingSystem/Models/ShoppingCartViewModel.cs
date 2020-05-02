using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketsBookingSystem.Models
{
    public class ShoppingCartViewModel
    {
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
        public PurchaseHeader PurchaseHeader { get; set; }
        public Payment Payment { get; set; }
    }
}
