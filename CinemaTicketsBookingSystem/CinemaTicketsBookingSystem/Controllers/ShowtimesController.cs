﻿using System;
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

        public async Task<IActionResult> Buy(int? id)
        {
            if (id == null) return NotFound();

            var showtime = await _db.Showtimes
                .Include(s => s.Movie)
                .Include(s => s.CinemaHall)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (showtime == null) return NotFound();

            Item item = new Item()
            {
                Showtime = showtime,
                ShowtimeId = showtime.Id
            };

            _db.Items.Add(item);
            _db.SaveChanges();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy([Bind("Id,ShowtimeId,Count")] Item item)
        {
            if (ModelState.IsValid)
            {
                var itemFromDb = _db.Items.FirstOrDefault(i => i.ShowtimeId == item.Id);
                itemFromDb.Count = item.Count;
                _db.Items.Update(itemFromDb);

                ShoppingCart shoppingCart;

                if (_db.ShoppingCarts.Any(s => s.IsPending))
                {
                    shoppingCart = _db.ShoppingCarts
                        .Include(s => s.Items)
                        .FirstOrDefault(s => s.IsPending);
                    shoppingCart.Items.Add(itemFromDb);
                }
                else
                {
                    shoppingCart = new ShoppingCart() { IsPending = true, Items = new List<Item>() };
                    shoppingCart.Items.Add(itemFromDb);
                    _db.ShoppingCarts.Add(shoppingCart);
                }
                _db.SaveChanges();
            }
            else
            {
                // TODO
            }

            return RedirectToAction(nameof(Index));
        }
    }
}