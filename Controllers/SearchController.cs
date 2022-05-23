using Fiore.Data;
using Fiore.Models;
using Fiore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiore.Controllers
{
    public class SearchController : Controller
    {
        private readonly FioreDbContext _context;

        public SearchController(FioreDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string keyword)
        {
            var products = _context.Products.Include(i => i.Category)
               .Where(i => i.ProductName.Contains(keyword) || 
                           i.Description.Contains(keyword) || 
                           i.Category.CategoryName.Contains(keyword));

            var response = new List<ProductViewModel>();
            if (products != null)
            {
                var entityProducts = _context.Products.Include(p => p.Category);
                var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");

                foreach (var product in products)
                {
                    var isInCart = cartItems == null ? false : 
                        cartItems.Any(i => i.Product.ProductId == product.ProductId);

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
            }

            return View(response);
        }
    }
}
