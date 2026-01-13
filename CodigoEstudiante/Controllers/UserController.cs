using Microsoft.AspNetCore.Mvc;
using CodigoEstudiante.Services;

namespace CodigoEstudiante.Controllers
{
    public class UserController(OrderService _orderService) : Controller
    {
        public async Task<IActionResult> MyOrders()
        {
            //TODO: change id
            var userId = 1;
            var ordersvm = await _orderService.GetAllByUserAsync(userId);
            return View(ordersvm);
        }
    }
}
