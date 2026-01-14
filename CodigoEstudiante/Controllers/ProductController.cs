using CodigoEstudiante.Models;
using CodigoEstudiante.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodigoEstudiante.Controllers
{
    public class ProductController(ProductService _productService) : Controller
    {
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }
        [HttpGet]
        public async Task<IActionResult> AddEdit(int id)
        {
            var productVM = await _productService.GetByIdAsync(id);
            return View(productVM);
        }
        [HttpPost]
        public async Task<IActionResult> AddEdit(ProductVM entityVM)
        {
            ViewBag.message = null;
            ModelState.Remove("Categories");
            ModelState.Remove("Category.Name");
            if (!ModelState.IsValid) return View(entityVM);

            if (entityVM.ProductId == 0)
            {
                await _productService.AddAsync(entityVM);
                ModelState.Clear();
                entityVM = new ProductVM();
                ViewBag.message = "Created Product";
            }
            else
            {
                await _productService.EditAsync(entityVM);
                ViewBag.message = "Edited Product";
            }
            return View(entityVM);
        }

        public async Task<ActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return RedirectToAction("index");
        }

    }
}

