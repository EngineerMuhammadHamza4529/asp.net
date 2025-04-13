using project_Admin_Dashboard_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BCrypt.Net;
using System.Threading.Tasks;
using System.Linq;

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
                return RedirectToAction("AdminDashboard", "Dashboard"); // ✅ Fixed redirection
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(User u)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Validation failed! Check your inputs.";
                return View();
            }

            // Find user by email
            var myuser = _context.Users.FirstOrDefault(x => x.Email == u.Email);

            if (myuser != null && BCrypt.Net.BCrypt.Verify(u.PasswordHash, myuser.PasswordHash)) // ✅ Fix password check
            {
                HttpContext.Session.SetString("UserSession", myuser.Email);
                return RedirectToAction("AdminDashboard", "Dashboard");
            }

            ViewBag.Message = "Invalid Email or Password!";
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
                // ✅ Hash the password before saving
                u.PasswordHash = BCrypt.Net.BCrypt.HashPassword(u.PasswordHash);

                await _context.Users.AddAsync(u);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Registered Successfully!";
                return RedirectToAction("Login");
            }

            ViewBag.Message = "Registration failed! Check inputs.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // ✅ Clear session
            return RedirectToAction("Login");
        }
    }
}
