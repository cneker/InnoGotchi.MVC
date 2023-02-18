using AutoMapper;
using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IMapper mapper, IAuthenticationService authenticationService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
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
            var userForAuth = await _authenticationService.RegisterUserAsync(userForReg);

            return View("~/Views/Authentication/SignIn.cshtml", userForAuth);
        }

        [HttpPost]  
        public async Task<IActionResult> SignIn(UserForAuthenticationDto userForAuth)
        {
            var token = await _authenticationService.SignInAsync(userForAuth);

            Response.Cookies.Append("jwt", token.AccessToken);

            var userForLayout = await _authenticationService.GetUserForLayoutAsync(token.AccessToken, token.UserId.ToString());

            Response.Cookies.Append("name", userForLayout.FirstName);
            Response.Cookies.Append("avatar", userForLayout.AvatarPath);

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
