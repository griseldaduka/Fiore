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
    public class ProductOrderDetailsController : Controller
    {
        private readonly FioreDbContext _context;

        public ProductOrderDetailsController(FioreDbContext context)
        {
            _context = context;
        }

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


        [HttpGet]
        public async Task<IActionResult> OrderProducts(int orderId)
        {
            List<OrderProductsViewModel> orderProductsList = new List<OrderProductsViewModel>();
            var orderProducts = await _context.ProductOrderDetails.Where(i => i.OrderId == orderId).ToListAsync();
            foreach(var oproduct in orderProducts)
            {
                var products = _context.Products.First(i => i.ProductId == oproduct.ProductId);
                orderProductsList.Add(new OrderProductsViewModel
                {
                    ImageName=products.ImageName,
                    ProductName = products.ProductName,
                    Quantity = oproduct.Quantity,
                    UnitPrice=oproduct.UnitPrice,
                    Subtotal = (oproduct.Quantity) * (oproduct.UnitPrice)
                }) ;
            }
            return View(orderProductsList);
        }
    }  
}
