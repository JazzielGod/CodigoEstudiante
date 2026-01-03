using CodigoEstudiante.Models;
using CodigoEstudiante.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodigoEstudiante.Controllers
{
    public class CategoryController(CategoryService _categoryService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(CategoryVM entityVM)
        {
            ViewBag.message = null;
            if (!ModelState.IsValid)
            {
                return View(entityVM);
            }
            await _categoryService.AddAsync(entityVM);
            ViewBag.Message = "Category added successfully!";
            return View();
        }
    }
}
