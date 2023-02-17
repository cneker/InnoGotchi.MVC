using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.MVC.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetService _petService;

        public PetController(IPetService petService)
        {
            _petService = petService;
        }

        public async Task<IActionResult> PetDetails(Guid farmId, Guid petId)
        {
            var jwt = Request.Cookies["jwt"];
            var userId = User.Claims.First(c => c.Type == "Id").Value;

            var pet = await _petService.GetPetDetailsAsync(jwt, userId, farmId.ToString(), petId.ToString());

            return View(new PetDetailsViewModel { PetDetails = pet, FarmId = farmId });
        }

        public async Task<IActionResult> Feed(Guid farmId, Guid petId)
        {
            var jwt = Request.Cookies["jwt"];
            var userId = User.Claims.First(c => c.Type == "Id").Value;

            await _petService.FeedAsync(jwt, userId, farmId.ToString(), petId.ToString());

            return RedirectToAction("PetDetails", new { farmId, petId });
        }

        public async Task<IActionResult> GiveADrink(Guid farmId, Guid petId)
        {
            var jwt = Request.Cookies["jwt"];
            var userId = User.Claims.First(c => c.Type == "Id").Value;

            await _petService.GiveADrinkAsync(jwt, userId, farmId.ToString(), petId.ToString());

            return RedirectToAction("PetDetails", new { farmId, petId });
        }
    }
}
