using System.Diagnostics;
using CodigoEstudiante.Models;
using CodigoEstudiante.Services;
using CodigoEstudiante.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CodigoEstudiante.Controllers
{
    public class HomeController(
        CategoryService _categoryService,
        ProductService _productService
        ) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            var products = await _productService.GetCatalogAsync();
            var catalog = new CatalogVM
            {
                Categories = categories,
                Products = products
            };
            return View(catalog);
        }
        public async Task<IActionResult> FilterByCategory(int id, string name)
        {
            var categories = await _categoryService.GetAllAsync();
            var products = await _productService.GetCatalogAsync(categoryId:id);
            var catalog = new CatalogVM { Categories = categories, Products = products, filterBy = name};
            return View("Index",catalog);
        }
        public async Task<IActionResult> FilterBySearch(string value)
        {
            var categories = await _categoryService.GetAllAsync();
            var products = await _productService.GetCatalogAsync(search: value);
            var catalog = new CatalogVM { Categories = categories, Products = products, filterBy = $"Results for: {value}" };
            return View("Index", catalog);
        }

        public async Task<IActionResult> ProductDetail(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> AddItemToCart(int productId, int quantity)
        {
            var product = await _productService.GetByIdAsync(productId);

            var cart = HttpContext.Session.Get<List<CartItemVM>>("Cart") ?? new List<CartItemVM>();

            if(cart.Find(x => x.ProductId == productId) == null)
            {
                cart.Add(new CartItemVM
                {
                    ProductId = productId,
                    ImageName = product.Name,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = quantity
                });
            }
            else
            {
                var updateProduct = cart.Find(x => x.ProductId == productId);
                updateProduct!.Quantity += quantity;
            }

            HttpContext.Session.Set("Cart", cart);
            ViewBag.Message = "Product added to cart successfully!";
            //return RedirectToAction("ProductDetail", new { id = productId });
            return View("ProductDetail", product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
