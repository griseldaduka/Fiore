using Microsoft.AspNetCore.Mvc;

namespace Fiore.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
