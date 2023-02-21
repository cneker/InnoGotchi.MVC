﻿using FluentValidation;
using FluentValidation.AspNetCore;
using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserService _userService;
        private readonly IValidator<UserForRegistrationDto> _userForRegvalidator;

        public AuthenticationController(IAuthenticationService authenticationService,
            IValidator<UserForRegistrationDto> userForRegvalidator,
            IUserService userService)
        {
            _authenticationService = authenticationService;
            _userForRegvalidator = userForRegvalidator;
            _userService = userService;
        }

        public IActionResult SignIn()
        {
            return View(new UserForAuthenticationDto());
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserForRegistrationDto userForReg)
        {
            var result = await _userForRegvalidator.ValidateAsync(userForReg);
            if (!result.IsValid)
            {
                result.AddToModelState(ModelState);
                return View(userForReg);
            }
            var userForAuth = await _authenticationService.RegisterUserAsync(userForReg, ModelState);
            if (!ModelState.IsValid)
            {
                return View(userForReg);
            }

            return View("~/Views/Authentication/SignIn.cshtml", userForAuth);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserForAuthenticationDto userForAuth)
        {
            var token = await _authenticationService.SignInAsync(userForAuth, ModelState);

            if (!ModelState.IsValid)
            {
                return View(userForAuth);
            }

            Response.Cookies.Append("jwt", token.AccessToken);

            var user = await _userService.GetUserInfoDtoAsync(token.AccessToken, token.UserId.ToString());

            Response.Cookies.Append("name", user.FirstName);
            Response.Cookies.Append("avatar", user.AvatarPath);

            return RedirectToAction("FarmOverview", "Farm");
        }

        public IActionResult LogOut()
        {
            Response.Cookies.Delete("jwt");
            Response.Cookies.Delete("name");
            Response.Cookies.Delete("avatar");
            Response.Cookies.Delete("my-farm-id");

            return RedirectToAction("Index", "Home");
        }
    }
}
