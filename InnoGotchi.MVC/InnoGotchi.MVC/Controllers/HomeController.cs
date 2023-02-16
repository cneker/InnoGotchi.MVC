﻿using InnoGotchi.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InnoGotchi.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
                return RedirectToAction("FarmOverview", "Farm");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AllInnoGotchi()
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