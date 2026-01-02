using System.Collections.ObjectModel;

namespace CodigoEstudiante.Entities

{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate{ get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public User? User { get; set; }
        public Collection<OrderItem> OrderItems { get; set; }
    }
}
