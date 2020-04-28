using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CinemaTicketsBookingSystem.Data;
using CinemaTicketsBookingSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketsBookingSystem.Controllers
{
    public class ShowtimesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ShowtimesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(int? id)
        {
            if (!_db.Showtimes.Any(s => s.Time.Date == DateTime.Today))
            {
                var movies = _db.Movies.OrderBy(m => Guid.NewGuid()).Take(6);

                var cinemaHalls = _db.CinemaHalls.ToList();
                int time = 15;
                int count = 1;
                decimal price;

                foreach (var movie in movies)
                {
                    var cinemaHall = (count <= 3) ? cinemaHalls[0] : cinemaHalls[1];
                    price = (time == 15) ? 49 : (time == 18) ? 69 : 89;
                    _db.Showtimes.Add(new Showtime
                    {
                        Time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, time, 0, 0),
                        MovieId = movie.Id,
                        CinemaHallId = cinemaHall.Id,
                        TicketPrice = price,
                        TicketsAvailable = cinemaHall.SeatingCapacity
                    });
                    count++;
                    time = (time == 21) ? 15 : time += 3;
                }
            }
            _db.SaveChanges();

            var showtimes = await _db.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.CinemaHall)
                .Where(s => s.Time.Day == DateTime.Now.Day)
                .OrderBy(s => s.Time).ToListAsync();

            if (id != null)
            {
                showtimes = showtimes.Where(s => s.Movie.Id == id).ToList();
            }

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                var count = _db.ShoppingCarts.Where(s => s.ApplicationUserId == claim.Value)
                    .ToList()
                    .Count();

                HttpContext.Session.SetInt32("Shopping cart session", count);
            }

            return View(showtimes);
        }

        public async Task<IActionResult> Buy(int? id)
        {
            if (id == null) return NotFound();

            var showtime = await _db.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.CinemaHall)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (showtime == null) return NotFound();

            ShoppingCart shoppingCart = new ShoppingCart()
            {
                Item = showtime,
                ItemId = showtime.Id
            };

            return View(shoppingCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Buy(ShoppingCart cartItem)
        {
            cartItem.Id = 0;

            if (ModelState.IsValid)
            {
                // ad to cart
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                cartItem.ApplicationUserId = claim.Value;

                ShoppingCart cartFromDb = _db.ShoppingCarts
                    .Include(s => s.Item)
                    .FirstOrDefault(s => s.ApplicationUserId == cartItem.ApplicationUserId && s.ItemId == cartItem.ItemId);

                if (cartFromDb == null)
                {
                    // No record for this user and this showtime item, create a new one
                    _db.ShoppingCarts.Add(cartItem);
                }
                else
                {
                    // Increase count of the existing showtime item
                    if (cartFromDb.Count + cartItem.Count <= 12)
                    {
                        cartFromDb.Count += cartItem.Count;
                        _db.ShoppingCarts.Update(cartFromDb);
                    }                    
                }
                _db.SaveChanges();

                var count = _db.ShoppingCarts
                    .Where(s => s.ApplicationUserId == cartItem.ApplicationUserId)
                    .ToList()
                    .Count();

                HttpContext.Session.SetInt32("Shopping cart session", count);

                return RedirectToAction(nameof(Index));
            }
            else if (cartItem.Count > 0)
            {
                var itemFromDb = _db.Showtimes
                    .Include(s => s.Movie)
                    .Include(s => s.CinemaHall)
                    .FirstOrDefault(s => s.Id == cartItem.ItemId);

                ShoppingCart shoppingCart = new ShoppingCart()
                {
                    Item = itemFromDb,
                    ItemId = itemFromDb.Id
                };

                return View(cartItem);
            }
            else
            {
                return View();
            }
        }
    }
}