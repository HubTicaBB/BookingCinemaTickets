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

        public async Task<IActionResult> Index()
        {
            var movies = _db.Movies.OrderBy(m => Guid.NewGuid()).Take(6);
            var cinemaHalls = _db.CinemaHalls.ToList();
            int time = 15;
            int count = 1;
            decimal price;

            if (!_db.Showtimes.Any(s => s.Time.Date == DateTime.Today))
            {
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

            var showtimes = _db.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.CinemaHall)
                .OrderBy(s => s.Time);

            return View(await showtimes.ToListAsync());
        }
    }
}