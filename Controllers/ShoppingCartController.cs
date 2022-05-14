using Fiore.Data;
using Fiore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly FioreDbContext _context;

        public ShoppingCartController(FioreDbContext context)
        {
            _context = context;
        }

        //[HttpPost]
        //public IActionResult AddItem(int? productId)
        //{
        //    var product = _context.Products.Find(productId);
        //    if (string.IsNullOrEmpty(HttpContext.Session.GetString("cart")))
        //    {
        //        //HttpContext.Session.SetString("cart", "The Doctor");
        //        List<Item> cart = new List<Item>();
        //        cart.Add(new Item { Product = product, Quantity = 1 });
        //        HttpContext.Session.SetString("cart", cart.ToString());
        //    }

        //        HttpContext.Session.SetString("cart", product.ProductName);
        //    return View();
        //}


        [HttpGet]
        public IActionResult ViewCart()
        {
            var cartItems = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddCartItem(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            if(SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                List<CartItem> cart = new List<CartItem>();
                cart.Add(new CartItem
                {
                    Product = product,
                    Quantity = 1
                });
                HttpContext.Session.SetObjectAsJson("cart", cart);
            }
            else
            {
                List<CartItem> sessionCart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
                sessionCart.Add(new CartItem
                {
                    Product = product,
                    Quantity = 1
                });
                HttpContext.Session.SetObjectAsJson("cart", sessionCart);
            }
           
            return RedirectToAction("Index", "Products");
        }
    }
}