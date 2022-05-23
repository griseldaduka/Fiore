using Fiore.Data;
using Microsoft.AspNetCore.Mvc;

namespace Fiore.Controllers
{
    [ApiController]
    [Route("api/Dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly FioreDbContext context;
        public DashboardController(FioreDbContext fioreDbContext)
        {
            context = fioreDbContext;
        }

        [HttpGet]
        public IActionResult GetLineGraphs()
        {
            var sales = context.ProductOrderDetails.GroupBy(a => a.ProductId)
                .Select(a => new 
                { 
                    Sales = a.Sum(b => b.Quantity), 
                    Name = a.Key
                }).OrderByDescending(b => b.Name).ToList(); 

            return Ok(sales);
        }
    }
}
