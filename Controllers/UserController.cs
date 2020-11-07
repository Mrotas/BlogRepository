﻿using BlogRepository.Domain.Interfaces;
using BlogRepository.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogRepository.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
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

            return RedirectToAction("Login");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel registerModel)
        {
            bool success = _userService.Register(registerModel);
            if (success)
            {
                return RedirectToAction("LogIn");
            }

            ViewBag.ErrorMessage = "Coś poszło nie tak. Proszę spróbować ponownie lub wybrać inną nazwę użytkownika.";
            return View();
        }
    }
}