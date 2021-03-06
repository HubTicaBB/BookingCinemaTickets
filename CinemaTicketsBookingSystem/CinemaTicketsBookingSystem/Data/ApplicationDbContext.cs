﻿using System;
using System.Collections.Generic;
using System.Text;
using CinemaTicketsBookingSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaTicketsBookingSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<CinemaHall> CinemaHalls { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Showtime> Showtimes { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<PurchaseHeader> PurchaseHeaders { get; set; }
        public DbSet<PurchaseDetails> PurchaseDetails { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CinemaHall>().ToTable("CinemaHalls");
            modelBuilder.Entity<Movie>().ToTable("Movies");
            modelBuilder.Entity<Genre>().ToTable("Genres");
            modelBuilder.Entity<Showtime>().ToTable("Showtimes");
            modelBuilder.Entity<ShoppingCart>().ToTable("ShoppingCarts");
            modelBuilder.Entity<PurchaseHeader>().ToTable("Purchases");
        }
    }
}
