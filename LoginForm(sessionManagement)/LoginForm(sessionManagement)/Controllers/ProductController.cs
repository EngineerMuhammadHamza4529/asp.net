using LoginForm_sessionManagement_.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LoginForm_sessionManagement_.Controllers
{
    public class ProductController : Controller
    {
        private readonly LoginFormContext _context;

        public ProductController(LoginFormContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            if (HttpContext.Session.GetString("UserSession") == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Product Added Successfully!";
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
