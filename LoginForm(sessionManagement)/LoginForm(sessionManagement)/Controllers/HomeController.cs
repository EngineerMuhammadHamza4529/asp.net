using LoginForm_sessionManagement_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.Scripting;

namespace LoginForm_sessionManagement_.Controllers
{
    public class HomeController : Controller
    {
        private readonly LoginFormContext _context;

        public HomeController(LoginFormContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Login(User u)
        {
            // (with Hashing password)

            //var myuser = _context.Users.FirstOrDefault(x => x.Email == u.Email);

            //if (myuser != null && BCrypt.Net.BCrypt.Verify(u.Password, myuser.Password))
            //{
            //    HttpContext.Session.SetString("UserSession", myuser.Email);
            //    return RedirectToAction("Dashboard");
            //}

            var myuser = _context.Users.Where(x => x.Email == u.Email && x.Password == u.Password).FirstOrDefault();
            if (myuser != null)
            {
                HttpContext.Session.SetString("UserSession", myuser.Email);
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.message = "Login Failed";
            }
            return View();
        }

        //public IActionResult Dashboard()
        //{
        //    if(HttpContext.Session.GetString("UserSession") != null)
        //    {
        //        ViewBag.mysession = HttpContext.Session.GetString("UserSession").ToString();
        //    }
        //    else
        //    {
        //        return RedirectToAction("Login");
        //    }
        //    return View();
        //}

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.mysession = HttpContext.Session.GetString("UserSession").ToString();
                ViewBag.Products = _context.Products.ToList();
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear session
            return RedirectToAction("Login"); // Redirect to login page
        }


        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User u)
        {
            if (ModelState.IsValid)
            {
                // Hash the password before saving
                // (with Hashing password)
                //u.Password = BCrypt.Net.BCrypt.HashPassword(u.Password);
                await _context.Users.AddAsync(u);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Registered Successfully!";
                return RedirectToAction("Login");
            }
            return View();
        }
    }
}
