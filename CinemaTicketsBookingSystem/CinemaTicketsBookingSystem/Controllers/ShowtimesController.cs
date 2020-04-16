using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaTicketsBookingSystem.Data;
using CinemaTicketsBookingSystem.Models;
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

            return View(showtimes);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var showtime = await _db.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.CinemaHall)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (showtime == null) return NotFound();

            ShoppingCart shoppingCart = new ShoppingCart()
            {
                Showtime = showtime,
                ShowtimeId = showtime.Id
            };

            return View(shoppingCart);
        }
    }
}