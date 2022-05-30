using Fiore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiore.Controllers
{
    [ApiController]
    [Route("api/Dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly FioreDbContext _context;
        public DashboardController(FioreDbContext fioreDbContext)
        {
            _context = fioreDbContext;
        }

        [HttpGet("GetProductSales")]
        public IActionResult GetProductSales()
        {
            var productOrderDetails = _context.ProductOrderDetails;
            var products = _context.Products;

            var joinedProductDetails =  from pod in productOrderDetails
                                        join p in products on pod.ProductId equals p.ProductId
                                        select new
                                        {
                                            ProductId = p.ProductId,
                                            ProductName = p.ProductName,
                                            Quantity = pod.Quantity
                                        };

            var result = joinedProductDetails.GroupBy(a => a.ProductName)
            .Select(a => new
            {
                Sales = a.Sum(b => b.Quantity),
                Name = a.Key
            }).OrderByDescending(b => b.Name).ToList();

            return Ok(result);
        }

        [HttpGet("GetProductCategorySales")]
        public IActionResult GetProductCategorySales()
        {
            var productOrderDetails = _context.ProductOrderDetails;
            var products = _context.Products.Include(p => p.Category);

            var joinedProductDetails =  from pod in productOrderDetails
                                        join p in products on pod.ProductId equals p.ProductId
                                        select new
                                        {
                                            ProductId = p.ProductId,
                                            ProductName = p.ProductName,
                                            ProductCategory = p.Category.CategoryName,
                                            Quantity = pod.Quantity
                                        };

            var result = joinedProductDetails.GroupBy(a => a.ProductCategory)
            .Select(a => new
            {
                Sales = a.Sum(b => b.Quantity),
                Name = a.Key
            }).OrderByDescending(b => b.Name).ToList();

            return Ok(result);
        }
    }
}
