using Microsoft.AspNetCore.Mvc;
using project_image_upload_and_Retrieveving_.Models;

namespace project_image_upload_and_Retrieveving_.Controllers
{
    public class ProductController : Controller
    {
        private readonly ImageUploadDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(ImageUploadDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Display all products
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products); // Pass list to view
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Photo == null || model.Photo.Length == 0)
                {
                    TempData["Error"] = "Please upload a valid image.";
                    return View(model);
                }

                // Optional: File size check (5MB max)
                if (model.Photo.Length > 5 * 1024 * 1024)
                {
                    TempData["Error"] = "File size is too large. Max allowed size is 5MB.";
                    return View(model);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Photo.FileName);
                var uploadPath = Path.Combine(_env.WebRootPath, "uploads");

                // Ensure the uploads folder exists
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var filePath = Path.Combine(uploadPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }

                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    ImagePath = "uploads/" + fileName // Save relative path
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Product added successfully!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Please correct the form errors and try again.";
            return View(model);
        }
    }
}
