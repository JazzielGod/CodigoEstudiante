using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace CodigoEstudiante.Entities
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Type { get; set; }

        public Collection<Order> Orders { get; set; }
    }
}
