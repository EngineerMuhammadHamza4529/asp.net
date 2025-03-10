using Microsoft.AspNetCore.Mvc;
using project_Admin_Dashboard_.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
namespace project_Admin_Dashboard_.Controllers
{
    public class AccountController : Controller
    {
        private readonly AdminDbContext _context;

        public AccountController(AdminDbContext context)
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
                return RedirectToAction("AdminDashboard" , "Dashboard");
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

            var myuser = _context.Users.Where(x => x.Email == u.Email && x.PasswordHash == u.PasswordHash).FirstOrDefault();
            if (myuser != null)
            {
                HttpContext.Session.SetString("UserSession", myuser.Email);
                return RedirectToAction("Dashboard", "Admin");

            }
            else
            {
                ViewBag.Message = "Login Failed";
            }
            return View();
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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear session
            return RedirectToAction("Login"); // Redirect to login page
        }
    }
}
