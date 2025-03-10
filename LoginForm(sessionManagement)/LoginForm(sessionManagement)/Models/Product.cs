using System.ComponentModel.DataAnnotations;

namespace LoginForm_sessionManagement_.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }
    }
}
