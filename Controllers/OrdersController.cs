using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fiore.Models;
using Fiore.Data;
using Fiore.Models.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Fiore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FioreDbContext _context;

        public OrdersController(UserManager<ApplicationUser> userManager, FioreDbContext context)
        {
            _userManager = userManager;
            _context = context;

        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AllOrders()
        {
            return View(await _context.Orders.ToListAsync());
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserOrders()
        {
            var userId = _userManager.GetUserId(User);
            return View(await _context.Orders.Where(i => i.UserId == userId).ToListAsync());
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int? id)
        {
            var userId = _userManager.GetUserId(User);
            if (_context.Orders != null)
            {
                if (id == null)
                {
                    var lastOrder = _context.Orders.OrderByDescending(i => i.OrderId).FirstOrDefault(u=>u.UserId==userId);
                    return View(lastOrder);
                }

                var order = await _context.Orders
                    .FirstOrDefaultAsync(m => m.OrderId == id);
                if (order == null)
                {
                    return NotFound();
                }
                return View(order);
            }
            return NotFound();
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Create(AddressViewModel order)
        {
            if (ModelState.IsValid)
            {
                var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
                decimal totalPrice = 0;

                foreach (var item in cartItems)
                {
                    totalPrice = totalPrice + item.Subtotal;
                }

                var userId = _userManager.GetUserId(User);

                _context.Orders.Add(new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    TotalPrice = totalPrice,
                    HouseNumber = order.HouseNumber,
                    StreetName = order.StreetName,
                    CityName = order.CityName,
                    CountryName = order.CountryName,

                });
                await _context.SaveChangesAsync();

                var actualOrder = _context.Orders.OrderByDescending(i => i.OrderId).FirstOrDefault();
                if (actualOrder != null)
                {
                    foreach (var item in cartItems)
                    {
                        _context.ProductOrderDetails.Add(new ProductOrderDetails
                        {
                            OrderId = actualOrder.OrderId,
                            ProductId = item.Product.ProductId,
                            Quantity = item.Quantity,
                            UnitPrice = item.Product.UnitPrice
                        });

                        var productToUpdate = _context.Products.First(p => p.ProductId == item.Product.ProductId);
                        productToUpdate.UnitsInStock = (productToUpdate.UnitsInStock) - (item.Quantity);
                        _context.Products.Update(productToUpdate);
                    }
                }
                else return View(order);

                await _context.SaveChangesAsync();
                HttpContext.Session.Remove("cart");
                return RedirectToAction("Details", "Orders", new { id = actualOrder.OrderId });
            }
            return RedirectToAction("Checkout", "ShoppingCart");
        }
    }
}
