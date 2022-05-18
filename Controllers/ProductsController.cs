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
    public class ProductsController : Controller
    {
        private readonly FioreDbContext _context;

        public ProductsController(FioreDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            List<ProductViewModel> productModel = new List<ProductViewModel>();
            var entityProducts = _context.Products.Include(p => p.Category);
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

            return View(productModel);
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
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }



        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        //    if (FileUpload.FormFile.Length > 0)
        //        { 
        //            using (var stream = new FileStream(Path.Combine(_hostenvironment.WebRootPath, "uploadfiles", FileUpload.FormFile.FileName), FileMode.Create))
        //            {
        //                await FileUpload.FormFile.CopyToAsync(stream);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind("CategoryId, ProductName,  Description , ImageName ,UnitPrice, UnitsInStock")] Product product)
        //{


        //        _context.Products.Add(new Product
        //        {
        //            CategoryId = product.CategoryId,
        //            ProductName = product.ProductName, 
        //            Description= product.Description,   
        //            ImageName= product.ImageName,   
        //            UnitPrice= product.UnitPrice,
        //            UnitsInStock= product.UnitsInStock,
        //            CreatedDate=DateTime.Now,
        //            UpdatedDate=DateTime.Now, 


        //        });
        //        _context.SaveChanges();
        //        return RedirectToAction("Index");


        //    return View(product);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("CategoryId, ProductName,  Description , Image ,UnitPrice, UnitsInStock")] ProductPostViewModel product)
        {
            if (ModelState.IsValid)
            {

                var category = _context.Categories.First(i => i.CategoryId == product.CategoryId);
                var ctgName = category.CategoryName;

                var directory = "wwwroot/Images/Products/" + ctgName;
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

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Products/Delete/5
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
