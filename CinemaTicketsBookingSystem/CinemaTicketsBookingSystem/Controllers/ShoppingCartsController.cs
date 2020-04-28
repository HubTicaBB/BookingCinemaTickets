using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CinemaTicketsBookingSystem.Data;
using CinemaTicketsBookingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;

namespace CinemaTicketsBookingSystem.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
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

            if (shoppingCart == null) return NotFound();

            if (shoppingCart.Count < 12)
            {
                shoppingCart.Count += 1;
            }

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

        public IActionResult Remove(int? cartId)
        {
            var shoppingCart = _db.ShoppingCarts.FirstOrDefault(s => s.Id == cartId);
            var countFromDb = _db.ShoppingCarts.Where(s => s.ApplicationUserId == shoppingCart.ApplicationUserId).ToList().Count();

            _db.ShoppingCarts.Remove(shoppingCart);
            _db.SaveChanges();

            HttpContext.Session.SetInt32("Shopping cart session", countFromDb - 1);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Checkout()
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

            ShoppingCartVM.PurchaseHeader.ApplicationUser = _db.Users.FirstOrDefault(u => u.Id == claim.Value);

            foreach (var item in ShoppingCartVM.ShoppingCarts)
            {
                ShoppingCartVM.PurchaseHeader.TotalAmount += (item.Item.TicketPrice * item.Count);
            }            

            return View(ShoppingCartVM);
        }

        public IActionResult PurchaseConfirmation()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVM = new ShoppingCartViewModel()
            {
                PurchaseHeader = new PurchaseHeader(),
                ShoppingCarts = _db.ShoppingCarts
                    .Include(s => s.Item)
                    .Include(s => s.Item.Movie)
                    .Include(s => s.Item.CinemaHall)
                    .Where(s => s.ApplicationUserId == claim.Value)
                    .ToList()
            };

            ShoppingCartVM.PurchaseHeader.ApplicationUser = _db.Users.FirstOrDefault(u => u.Id == claim.Value);
            ShoppingCartVM.PurchaseHeader.UserName = ShoppingCartVM.PurchaseHeader.ApplicationUser.UserName;
            ShoppingCartVM.PurchaseHeader.Status = "Completed";
            ShoppingCartVM.PurchaseHeader.PaymentStatus = "Completed";
            ShoppingCartVM.PurchaseHeader.ApplicationUserId = claim.Value;
            ShoppingCartVM.PurchaseHeader.Date = DateTime.Now;

            _db.PurchaseHeaders.Add(ShoppingCartVM.PurchaseHeader);
            _db.SaveChanges();

            List<PurchaseDetails> purchaseDetailsList = new List<PurchaseDetails>();
            foreach (var item in ShoppingCartVM.ShoppingCarts)
            {
                PurchaseDetails purchaseDetails = new PurchaseDetails()
                {
                    ItemId = item.Item.Id,
                    PurchaseId = ShoppingCartVM.PurchaseHeader.Id,
                    Price = item.Item.TicketPrice,
                    Count = item.Count
                };
                ShoppingCartVM.PurchaseHeader.TotalAmount += purchaseDetails.Count * purchaseDetails.Price;
                _db.PurchaseDetails.Add(purchaseDetails);

                var itemFromDb = _db.Showtimes.FirstOrDefault(s => s.Id == item.Item.Id);
                itemFromDb.TicketsAvailable -= item.Count;
                _db.Showtimes.Update(itemFromDb);
            }

            _db.ShoppingCarts.RemoveRange(ShoppingCartVM.ShoppingCarts);
            _db.SaveChanges();
            HttpContext.Session.SetInt32("Shopping cart session", 0);
            return View();
        }
    }
}