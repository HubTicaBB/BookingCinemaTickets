﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaTicketsBookingSystem.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public IList<Item> Items { get; set; }
        public bool IsPending { get; set; }
    }
}
