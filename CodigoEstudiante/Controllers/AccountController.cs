using CodigoEstudiante.Models;
using CodigoEstudiante.Services;
using Microsoft.AspNetCore.Mvc;

namespace CodigoEstudiante.Controllers
{
    public class AccountController(UserService _userService) : Controller
    {
        public IActionResult Login() 
        {
            var viewModel = new LoginVM();
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM viewmodels)
        {
            if (!ModelState.IsValid) return View(viewmodels);
            var found = await _userService.Login(viewmodels);

            if (found.UserId == 0)
            {
                ViewBag.message = "No matches found";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult Register()
        {
            var viewModel = new UserVM();
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserVM viewmodel)
        {
            if (!ModelState.IsValid) return View(viewmodel);
            try
            {
                await _userService.Register(viewmodel);
                ViewBag.message = "Your account has been registered, please try loggin in";
                ViewBag.Class = "alert-success";
            }
            catch (Exception ex)
            {
                ViewBag.message = ex.Message;
                ViewBag.Class = "alert-danger";
            }

            return View();
           
        }
    }
}
