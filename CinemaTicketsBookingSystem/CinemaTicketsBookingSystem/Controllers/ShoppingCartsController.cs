using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CinemaTicketsBookingSystem.Controllers
{
    public class ShoppingCartsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}