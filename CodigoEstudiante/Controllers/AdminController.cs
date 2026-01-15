using CodigoEstudiante.Models;
using CodigoEstudiante.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CodigoEstudiante.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController(
        CategoryService _categoryService,
        ProductService _productService
    ) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            var products = await _productService.GetAllAsync();

            var viewModel = new DashboardVM
            {
                CategoryCount = categories.Count(),
                ProductCount = products.Count()
            };

            return View(viewModel);
        }
    }
}