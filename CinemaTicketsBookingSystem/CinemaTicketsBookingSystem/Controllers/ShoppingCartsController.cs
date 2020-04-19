using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CinemaTicketsBookingSystem.Data;
using CinemaTicketsBookingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketsBookingSystem.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]  // Otherwise OrderHeader is considered as not initialized in the POST action method somehow...
        public ShoppingCartViewModel ShoppingCartVM { get; set; }

        public ShoppingCartsController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM = new ShoppingCartViewModel()
            {
                PurchaseHeader = new PurchaseHeader(),
                ShoppingCarts = await _db.ShoppingCarts
                    .Include(s => s.Item)
                    .Include(s => s.Item.Movie)
                    .Include(s => s.Item.CinemaHall)
                    .Where(s => s.ApplicationUserId == claim.Value)
                    .ToListAsync()
            };

            ShoppingCartVM.PurchaseHeader.TotalAmount = 0;
            ShoppingCartVM.PurchaseHeader.ApplicationUser = _db.Users.FirstOrDefault(u => u.Id == claim.Value);

            foreach (var item in ShoppingCartVM.ShoppingCarts)
            {
                ShoppingCartVM.PurchaseHeader.TotalAmount += (item.Item.TicketPrice * item.Count);
            }

            return View(ShoppingCartVM);
        }

        public IActionResult Plus(int? cartid)
        {
            if (cartid == null) return NotFound();

            var shoppingCart = _db.ShoppingCarts.FirstOrDefault(s => s.Id == cartid);
            shoppingCart.Count += 1;

            if (shoppingCart == null) return NotFound();

            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int? cartid)
        {
            if (cartid == null) return NotFound();

            var shoppingCart = _db.ShoppingCarts.FirstOrDefault(s => s.Id == cartid);

            if (shoppingCart == null) return NotFound();

            if (shoppingCart.Count == 1)
            {
                var countFromDb = _db.ShoppingCarts.Where(s => s.ApplicationUserId == shoppingCart.ApplicationUserId).ToList().Count();
                _db.ShoppingCarts.Remove(shoppingCart);
                _db.SaveChanges();
                HttpContext.Session.SetInt32("Shopping cart session", countFromDb - 1);
            }
            else
            {
                shoppingCart.Count -= 1;
                _db.SaveChanges();
            }            

            if (shoppingCart == null) return NotFound();

            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Checkout()
        {
            return View();
        }


    //    ShoppingCartVM.PurchaseHeader.ApplicationUser = _db.Users.FirstOrDefault(u => u.Id == claim.Value);

    //        foreach (var item in ShoppingCartVM.ShoppingCarts)
    //        {
    //            ShoppingCartVM.PurchaseHeader.TotalAmount += (item.Price* item.Count);
    //        }

    //ShoppingCartVM.PurchaseHeader.UserName = ShoppingCartVM.PurchaseHeader.ApplicationUser.UserName;

    //        return View(ShoppingCartVM);
}
}