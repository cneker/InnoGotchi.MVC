using AutoMapper;
using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.Models.Pet;
using InnoGotchi.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.MVC.Controllers
{
    public class PetController : Controller
    {
        private readonly IPetService _petService;
        private readonly IMapper _mapper;

        public PetController(IPetService petService, IMapper mapper)
        {
            _petService = petService;
            _mapper = mapper;
        }

        private (string, string) GetRequiredParameters()
        {
            var jwt = Request.Cookies["jwt"];
            var userId = User.Claims.First(c => c.Type == "Id").Value;
            return (jwt, userId);
        }

        public async Task<IActionResult> PetDetails(Guid farmId, Guid petId)
        {
            var (jwt, userId) = GetRequiredParameters();

            var pet = await _petService.GetPetDetailsAsync(jwt, userId, farmId, petId);

            var petDetailsVM = new PetDetailsViewModel
            {
                PetDetails = pet,
                PetForUpdate = _mapper.Map<PetForUpdateDto>(pet),
                FarmId = farmId
            };

            return View(petDetailsVM);
        }

        public async Task<IActionResult> Feed(Guid farmId, Guid petId)
        {
            var (jwt, userId) = GetRequiredParameters();

            await _petService.FeedAsync(jwt, userId, farmId, petId);

            return RedirectToAction("PetDetails", new { farmId, petId });
        }

        public async Task<IActionResult> GiveADrink(Guid farmId, Guid petId)
        {
            var (jwt, userId) = GetRequiredParameters();

            await _petService.GiveADrinkAsync(jwt, userId, farmId, petId);

            return RedirectToAction("PetDetails", new { farmId, petId });
        }

        public async Task<IActionResult> UpdatePet(Guid farmId, Guid petId, PetDetailsViewModel petVM)
        {
            var (jwt, userId) = GetRequiredParameters();

            await _petService.UpdatePetAsync(jwt, userId, farmId, petId, petVM.PetForUpdate);

            return RedirectToAction("PetDetails", new { farmId, petId });
        }
    }
}
