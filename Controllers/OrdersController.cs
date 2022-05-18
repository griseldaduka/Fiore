using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fiore.Models;
using Fiore.Data;
using Fiore.Models.ViewModels;

namespace Fiore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly FioreDbContext _context;

        public OrdersController(FioreDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
              return View();
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( OrderViewModel order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderViewModel order)
        {
            var existingOrder = await _context.Orders.FindAsync(id);    
            if(existingOrder != null)
            {
                if (ModelState.IsValid)
                {
                    //existingOrder.OrderId=order.OrderId; 
                    //existingOrder.UserId=order.UserId;  
                    //existingOrder.TotalPrice=order.TotalPrice;
                    //existingOrder.OrderDate=order.OrderDate;  
                    existingOrder.HouseNumber=order.HouseNumber;    
                    existingOrder.StreetName=order.StreetName;  
                    existingOrder.CityName=order.CityName;  
                    existingOrder.CountryName=order.CountryName;    

                    //try
                    //{
                    //    _context.Update(order);
                    //    await _context.SaveChangesAsync();
                    //}
                    //catch (DbUpdateConcurrencyException)
                    //{
                    //    if (!OrderExists(order.OrderId))
                    //    {
                    //        return NotFound();
                    //    }
                    //    else
                    //    {
                    //        throw;
                    //    }
                    //}
                    //return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'FioreDbContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
