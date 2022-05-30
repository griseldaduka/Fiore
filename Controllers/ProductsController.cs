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
    public class ProductsController : Controller
    {
        private readonly FioreDbContext _context;

        public ProductsController(FioreDbContext context)
        {
            _context = context;
        }
        // GET: Products
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            if (product != null)
            {
                var response = new ProductViewModel();
                var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>("cart");
                if (cartItems != null)
                {
                    foreach (var item in cartItems)
                    {
                        if (item.Product.ProductId == product.ProductId)
                        {
                            response=new ProductViewModel
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
                                IsInCart = true,
                                Category = product.Category,
                            };
                        }
                        else
                        {
                            response=new ProductViewModel
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
                                IsInCart = false,
                                Category = product.Category,
                            };
                        }
                    }
                    return View(response);
                }
                else
                {
                    response=new ProductViewModel
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
                        IsInCart = false,
                        Category = product.Category,
                    };
                    return View(response);
                }
            }
            return BadRequest("Product Not Found");
        }



        [Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([Bind("CategoryId, ProductName,  Description , Image ,UnitPrice, UnitsInStock")] ProductPostViewModel product)
        {
            if (ModelState.IsValid)
            {

                var category = _context.Categories.First(i => i.CategoryId == product.CategoryId);
                var ctgName = category.CategoryName;

                var directory = "wwwroot/img/Products/" + ctgName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), directory);

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string fileName = product.Image.FileName;
                string fileNameWithPath = Path.Combine(path, fileName);


                _context.Products.Add(new Product
                {
                    CategoryId = product.CategoryId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    ImageName = fileName,
                    UnitPrice = product.UnitPrice,
                    UnitsInStock = product.UnitsInStock,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,


                });
                _context.SaveChanges();
                using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                {
                    product.Image.CopyTo(stream);
                }
                return RedirectToAction("Index");
            }


            return View(product);
        }


        [Authorize(Roles = "Admin")]
        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, ProductViewModel product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct != null)
            {
                if (ModelState.IsValid)
                {
                    existingProduct.ProductId = product.ProductId;
                    existingProduct.CategoryId = product.CategoryId;
                    existingProduct.ProductName = product.ProductName;
                    existingProduct.Description = product.Description;
                    existingProduct.ImageName = product.ImageName;
                    existingProduct.UnitPrice = product.UnitPrice;
                    existingProduct.UnitPrice = product.UnitPrice;
                    existingProduct.CreatedDate = product.CreatedDate;
                    existingProduct.UpdatedDate = product.UpdatedDate;

                    try
                    {
                        _context.Update(existingProduct);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(product.ProductId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'FioreDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
    }
}
