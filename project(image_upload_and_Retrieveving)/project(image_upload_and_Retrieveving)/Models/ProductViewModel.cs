using System.ComponentModel.DataAnnotations;

namespace project_image_upload_and_Retrieveving_.Models
{
    public class ProductViewModel
    {
        
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int Price { get; set; }
        [Required]
        public IFormFile Photo { get; set; } = null!;
    }
}
