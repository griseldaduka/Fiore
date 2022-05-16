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
        
    }  
}
