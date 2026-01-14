using CodigoEstudiante.Models;
using CodigoEstudiante.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

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
                List<Claim> claims = new List<Claim>(){
                    new Claim(ClaimTypes.NameIdentifier, found.UserId.ToString()),
                    new Claim(ClaimTypes.Name, found.FullName),
                    new Claim(ClaimTypes.Email, found.Email),
                    new Claim(ClaimTypes.Role, found.Type)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme
                );

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                    }
                );

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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Index", "Home");
        }
    }
}
