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
        public IActionResult Index(string? order)
        {
            if (!String.IsNullOrEmpty(Request.Cookies["Search"]))
            {
                ViewBag.Cookie= Request.Cookies["Search"];
            }
            var response = new List<ProductViewModel>();
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");

            if (order == "asc")
            {
                var entityProducts =  _context.Products.Include(p => p.Category).OrderBy(u => u.UnitPrice);
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
                        Category = product.Category,
                    });
                }
                return View(response);
            }
            else if (order == "dsc")
            {
                var entityProducts = _context.Products.Include(p => p.Category).OrderByDescending(u => u.UnitPrice);
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
                        Category = product.Category,
                    });
                }
                return View(response);
            }
            else
            {
                var entityProducts = _context.Products.Include(p => p.Category);
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
                        Category = product.Category,
                    });
                }
                return View(response);
            }
        }
        public IActionResult Error404()
        {
            return View();
        }
      
    }

}
