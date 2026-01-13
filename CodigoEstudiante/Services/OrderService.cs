using CodigoEstudiante.Repositories;
using CodigoEstudiante.Models;
using CodigoEstudiante.Entities;
namespace CodigoEstudiante.Services
{
    public class OrderService(OrderRepository _orderRepository)
    {
        public async Task AddAsync(List<CartItemVM> cartItemVM, int userId)
        {
            Order order = new Order()
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = cartItemVM.Sum(x => x.Price * x.Quantity),
                OrdenItems = cartItemVM.Select(x => new OrderItem
                {
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()
            }; 
            await _orderRepository.AddAsync(order);
        }
    }
}
