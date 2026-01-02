using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CodigoEstudiante.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }

        public Collection<Product> Products { get; set; }
    }
}
