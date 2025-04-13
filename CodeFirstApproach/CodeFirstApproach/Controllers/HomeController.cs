using Microsoft.AspNetCore.Mvc;

namespace CodeFirstApproach.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
