using BlogRepository.Domain.Interfaces;
using BlogRepository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogRepository.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserAuthenticationService _userService;

        public UserController(IUserAuthenticationService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(LoginModel loginModel)
        {
            LoginResultModel loginResultModel = _userService.LogIn(loginModel);
            if (loginResultModel.LogInSuccess)
            {
                HttpContext.Session.SetInt32("UserId", loginResultModel.UserId);
                return RedirectToAction("Index", "Post", loginResultModel.UserId);
            }

            ViewBag.ErrorMessage = "Podano błędny login/hasło. Proszę spróbować ponownie.";
            return View();
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("UserId");

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {
            RegisterResultModel registerResultModel = _userService.Register(registerModel);
            if (registerResultModel.RegisterSuccess)
            {
                HttpContext.Session.SetInt32("UserId", registerResultModel.UserId);
                return RedirectToAction("Index", "Post", registerResultModel.UserId);
            }

            ViewBag.ErrorMessage = "Coś poszło nie tak. Proszę spróbować ponownie lub wybrać inną nazwę użytkownika.";
            return View();
        }
    }
}