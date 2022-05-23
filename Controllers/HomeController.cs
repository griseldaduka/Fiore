using Fiore.Data;
using Fiore.Models;
using Fiore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Fiore.Controllers
{
    public class HomeController : Controller
    {
        private readonly FioreDbContext _context;

        public HomeController(FioreDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<ProductViewModel> response = new List<ProductViewModel>();
            var entityProducts = _context.Products.Include(p => p.Category);
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");

            foreach (var product in entityProducts)
            {
                var isInCart = cartItems == null ? false : cartItems.Any(i => i.Product.ProductId == product.ProductId);

                response.Add(new ProductViewModel
                {
                    ProductId = product.ProductId,
                    CategoryId = product.CategoryId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    ImageName = product.ImageName,
                    UnitPrice = product.UnitPrice,
                    UnitsInStock = product.UnitsInStock,
                    CreatedDate = product.CreatedDate,
                    UpdatedDate = product.UpdatedDate,
                    IsInCart = isInCart,
                });
            }

            return View(response);
        }
    }
}