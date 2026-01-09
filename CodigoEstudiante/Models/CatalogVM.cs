namespace CodigoEstudiante.Models
{
    public class CatalogVM
    {
        public IEnumerable<CategoryVM> Categories { get; set; }
        public IEnumerable<ProductVM> Products{ get; set; }
        public String filterBy { get; set; }
    }
}
