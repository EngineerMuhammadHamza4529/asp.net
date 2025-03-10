using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using project_session_management_.Models;
using Microsoft.AspNetCore.Http;

namespace project_session_management_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetString("mykey", "hello world");
            return View();
        }

        public IActionResult About()
        {
            if (HttpContext.Session.GetString("mykey") != null)
            {
                ViewBag.data = HttpContext.Session.GetString("mykey").ToString();
            }
            return View();
        }

        public IActionResult Detail()
        {
            if (HttpContext.Session.GetString("mykey") != null)
            {
                ViewBag.data = HttpContext.Session.GetString("mykey").ToString();
            }
            return View();
        }


        public IActionResult injectsession()
        {
            
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("mykey") != null)
            {
                 HttpContext.Session.Remove("mykey");
            }

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
