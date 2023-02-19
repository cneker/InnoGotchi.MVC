﻿using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.Models;
using InnoGotchi.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InnoGotchi.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPetService _petService;

        public HomeController(ILogger<HomeController> logger, IPetService petService)
        {
            _logger = logger;
            _petService = petService;
        }

        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
                return RedirectToAction("FarmOverview", "Farm");
            return View();
        }

        public async Task<IActionResult> AllInnoGotchi(int pageNumber, int orderBy, int pageSize)
        {
            var jwt = Request.Cookies["jwt"];

            var orderByString = orderBy switch
            {
                1 => "happynessdaycount",
                2 => "age",
                3 => "hungerlevel",
                4 => "thirstylevel",
                _ => "happynessdaycount"
            };

            var petPaging = await _petService.GetPetsAsync(jwt, pageNumber.ToString(), orderByString, pageSize.ToString());

            var allPetsVM = new AllPetsViewModel
            {
                PetPaging = petPaging,
                RequestParameters = new()
                {
                    CurrentPage = pageNumber,
                    PageSize = pageSize,
                    OrderType = orderBy
                }
            };

            return View(allPetsVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SetupQueryParameters(AllPetsViewModel allPetsVM)
        {
            var pageNumber = allPetsVM.RequestParameters.CurrentPage;
            var pageSize = allPetsVM.RequestParameters.PageSize;
            var orderBy = allPetsVM.RequestParameters.OrderType;

            return RedirectToAction("AllInnoGotchi", new { pageNumber, orderBy, pageSize });
        }
    }
}