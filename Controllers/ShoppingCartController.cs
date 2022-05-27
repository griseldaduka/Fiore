using Fiore.Data;
using Fiore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly FioreDbContext _context;

        public ShoppingCartController(FioreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ViewCart()
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            return View(cartItems);
        }

        [HttpPost]
        public IActionResult AddCartItem(int id, int quantity)
        {
            var product = _context.Products.Include(p=>p.Category).First(i=>i.ProductId==id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                if (product.UnitsInStock == 0)
                {
                    return BadRequest("This product is not available");
                }

                if (product.UnitsInStock - quantity < 0)
                {
                    return BadRequest("Selected quantity for this product is over units in stock");
                }

                if (HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart") == null)
                {
                    List<CartItem> cart = new List<CartItem>();
                    cart.Add(new CartItem
                    {
                        Product = product,
                        Quantity = quantity,
                        Subtotal = product.UnitPrice * quantity
                    });
                    HttpContext.Session.SetObjectAsJson("cart", cart);
                }
                else
                {
                    List<CartItem> sessionCart = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
                    sessionCart.Add(new CartItem
                    {
                        Product = product,
                        Quantity = quantity,
                        Subtotal = product.UnitPrice * quantity

                    });
                    HttpContext.Session.SetObjectAsJson("cart", sessionCart);
                }

                return Ok();
            }
        }

        public IActionResult RemoveCartItem(int id)
        {
            var list = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            foreach (var item in list)
            {
                if (item.Product.ProductId == id)
                {
                    list.Remove(item);
                    break;
                }
            }
            HttpContext.Session.SetObjectAsJson("cart", list);
            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            List<CartItem> list = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            if (list == null)
            {
                return BadRequest();
            }
            foreach (var item in list)
            {
                if (item.Product.ProductId == id)
                {
                    item.Quantity = quantity;
                    item.Subtotal = item.Product.UnitPrice * quantity;
                    break;
                }
            }

            HttpContext.Session.SetObjectAsJson("cart", list);
            return RedirectToAction("ViewCart");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
            return View(cartItems);
        }
    }
}