using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaTicketsBookingSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketsBookingSystem.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ShoppingCartsController(ApplicationDbContext db)
        {
            _db = db;
        }

        //public async Task<IActionResult> Index()
        //{
        //    var shoppingCart = await _db.ShoppingCarts
        //        .Include(s => s.Items)
        //        .FirstOrDefaultAsync(s => s.IsPending);

        //    return View(shoppingCart);
        //}
    }
}