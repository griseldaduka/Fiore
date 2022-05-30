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
using Microsoft.AspNetCore.Authorization;

namespace Fiore.Controllers
{
    public class ProductOrderDetailsController : Controller
    {
        private readonly FioreDbContext _context;

        public ProductOrderDetailsController(FioreDbContext context)
        {
            _context = context;
        }

        [Authorize]
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> OrderProducts(int orderId)
        {
            var orderProductsList = new List<CartItem>();
            var orderProducts = await _context.ProductOrderDetails.Where(i => i.OrderId == orderId).ToListAsync();
            foreach(var oproduct in orderProducts)
            {
                var product = _context.Products.Include(i=>i.Category).First(i => i.ProductId == oproduct.ProductId);
                orderProductsList.Add(new CartItem
                {
                    //ImageName=products.ImageName,
                    //ProductName = products.ProductName,
                    Product = product,
                    Quantity = oproduct.Quantity,
                    //UnitPrice=oproduct.UnitPrice,
                    Subtotal = (oproduct.Quantity) * (oproduct.UnitPrice)
                }) ;
            }
            return View(orderProductsList);
        }
    }  
}
