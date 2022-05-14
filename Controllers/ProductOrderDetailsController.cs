using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fiore.Models;
using Fiore.Data;

namespace Fiore.Controllers
{
    public class ProductOrderDetailsController : Controller
    {
        private readonly FioreDbContext _context;

        public ProductOrderDetailsController(FioreDbContext context)
        {
            _context = context;
        }

        // GET: ProductOrderDetails
        public async Task<IActionResult> Index()
        {
              //return _context.Movie != null ? 
              //            View(await _context.Movie.ToListAsync()) :
              //            Problem("Entity set 'FioreDbContext.ProductOrderDetails'  is null.");
              return View();
        }

        // GET: ProductOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductOrderDetails == null)
            {
                return NotFound();
            }

            var productOrderDetails = await _context.ProductOrderDetails
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (productOrderDetails == null)
            {
                return NotFound();
            }

            return View(productOrderDetails);
        }

        // GET: ProductOrderDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductOrderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("OrderId,ProductId,UnitPrice,Quantity")] ProductOrderDetails productOrderDetails)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(productOrderDetails);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(productOrderDetails);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,ProductId,UnitPrice,Quantity")] ProductOrderDetails productOrderDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productOrderDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productOrderDetails);
        }
        // GET: ProductOrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductOrderDetails == null)
            {
                return NotFound();
            }

            var productOrderDetails = await _context.ProductOrderDetails.FindAsync(id);
            if (productOrderDetails == null)
            {
                return NotFound();
            }
            return View(productOrderDetails);
        }

        // POST: ProductOrderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("OrderId,ProductId,UnitPrice,Quantity")] ProductOrderDetails productOrderDetails)
        {
            if (id != productOrderDetails.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productOrderDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductOrderDetailsExists(productOrderDetails.OrderId))
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
            return View(productOrderDetails);
        }

        // GET: ProductOrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductOrderDetails == null)
            {
                return NotFound();
            }

            var productOrderDetails = await _context.ProductOrderDetails
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (productOrderDetails == null)
            {
                return NotFound();
            }

            return View(productOrderDetails);
        }

        // POST: ProductOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.ProductOrderDetails == null)
            {
                return Problem("Entity set 'FioreDbContext.ProductOrderDetails'  is null.");
            }
            var productOrderDetails = await _context.ProductOrderDetails.FindAsync(id);
            if (productOrderDetails != null)
            {
                _context.ProductOrderDetails.Remove(productOrderDetails);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductOrderDetailsExists(int? id)
        {
          return (_context.ProductOrderDetails?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
