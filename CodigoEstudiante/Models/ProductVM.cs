using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CodigoEstudiante.Models
{
    public class ProductVM
    {
        public int ProductId { get; set; }
        public CategoryVM Category { get; set; }

        public List<SelectListItem> Categories { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int stock { get; set; }

        public string? ImageName { get; set; } = null;

        public IFormFile? ImageFile { get; set; }
    }
}
