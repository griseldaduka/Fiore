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
        public async Task<IActionResult> Index(string? filtro)
        {

            List<ProductViewModel> productModel = new List<ProductViewModel>();
            if (filtro == null)
            {
                var entityProducts = _context.Products.Include(p => p.Category);
                var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");

                foreach (var product in entityProducts)
                {
                    var isInCart = cartItems == null ? false : cartItems.Any(i => i.Product.ProductId == product.ProductId);

                    productModel.Add(new ProductViewModel
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
                return View(productModel);
            }

            else
            {
                var category = _context.Categories.First(i => i.CategoryName == filtro);
                var ctgId = category.CategoryId;
                var entityProducts = _context.Products.Where(i => i.CategoryId == ctgId);



                var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");

                if (entityProducts == null)
                {
                    return View(productModel);
                }

                foreach (var product in entityProducts)
                {
                    var isInCart = cartItems == null ? false : cartItems.Any(i => i.Product.ProductId == product.ProductId);

                    productModel.Add(new ProductViewModel
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

            return View(productModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}