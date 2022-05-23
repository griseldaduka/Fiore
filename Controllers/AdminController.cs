using Microsoft.AspNetCore.Mvc;

namespace Fiore.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Graph()
        {
            return View();
        }
    }
}
