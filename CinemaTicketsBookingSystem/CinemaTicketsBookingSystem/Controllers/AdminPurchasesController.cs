using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaTicketsBookingSystem.Data;
using CinemaTicketsBookingSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace CinemaTicketsBookingSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPurchasesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminPurchasesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AdminPurchases
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PurchaseHeaders.Include(p => p.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdminPurchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseHeader = await _context.PurchaseHeaders
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseHeader == null)
            {
                return NotFound();
            }

            return View(purchaseHeader);
        }

        // GET: AdminPurchases/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: AdminPurchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ApplicationUserId,Date,TotalAmount,Status,PaymentStatus,UserName")] PurchaseHeader purchaseHeader)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseHeader);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", purchaseHeader.ApplicationUserId);
            return View(purchaseHeader);
        }

        // GET: AdminPurchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseHeader = await _context.PurchaseHeaders.FindAsync(id);
            if (purchaseHeader == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", purchaseHeader.ApplicationUserId);
            return View(purchaseHeader);
        }

        // POST: AdminPurchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ApplicationUserId,Date,TotalAmount,Status,PaymentStatus,UserName")] PurchaseHeader purchaseHeader)
        {
            if (id != purchaseHeader.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseHeader);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseHeaderExists(purchaseHeader.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", purchaseHeader.ApplicationUserId);
            return View(purchaseHeader);
        }

        // GET: AdminPurchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseHeader = await _context.PurchaseHeaders
                .Include(p => p.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (purchaseHeader == null)
            {
                return NotFound();
            }

            return View(purchaseHeader);
        }

        // POST: AdminPurchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseHeader = await _context.PurchaseHeaders.FindAsync(id);
            _context.PurchaseHeaders.Remove(purchaseHeader);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseHeaderExists(int id)
        {
            return _context.PurchaseHeaders.Any(e => e.Id == id);
        }
    }
}
