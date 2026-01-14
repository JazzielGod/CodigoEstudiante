using CodigoEstudiante.Models;
using CodigoEstudiante.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodigoEstudiante.Controllers
{
    public class CategoryController(CategoryService _categoryService) : Controller
    {
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            var categoryVM = await _categoryService.GetByIdAsync(id);
            return View(categoryVM);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(CategoryVM entityVM)
        {
            ViewBag.message = null;
            if (!ModelState.IsValid) return View(entityVM);
            if(entityVM.CategoryId == 0)
            {
                await _categoryService.AddAsync(entityVM);
                ModelState.Clear();
                entityVM = new CategoryVM();
                ViewBag.message = "Created Cateogory";
            }
            else
            {
                await _categoryService.EditAsync(entityVM);
                ViewBag.message = "Edited Category";
            }
            return View(entityVM);
        }

        public async Task<ActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction("index");
        }
    }
}
