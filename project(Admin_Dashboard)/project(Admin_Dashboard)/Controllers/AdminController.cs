using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace project_Admin_Dashboard_.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.mysession = HttpContext.Session.GetString("UserSession").ToString();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}
