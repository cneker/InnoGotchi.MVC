using AutoMapper;
using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.Models.Farm;
using InnoGotchi.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.MVC.Controllers
{
    [Authorize]
    public class FarmController : Controller
    {
        private readonly IFarmService _farmService;
        private readonly IPetService _petService;
        private readonly IMapper _mapper;

        public FarmController(IFarmService farmService, IPetService petService, IMapper mapper)
        {
            _farmService = farmService;
            _petService = petService;
            _mapper = mapper;
        }

        private (string, string) GetRequiredParameters()
        {
            var jwt = Request.Cookies["jwt"];
            var userId = User.Claims.First(c => c.Type == "Id").Value;
            return (jwt, userId);
        }

        [HttpGet]
        public async Task<IActionResult> FarmOverview()
        {
            var (jwt, userId) = GetRequiredParameters();

            var farmDto = await _farmService.GetUserFarmOverviewAsync(jwt, userId);
            Response.Cookies.Append("my-farm-id", farmDto == null ? "" : farmDto.Id.ToString());
            var farms = await _farmService.GetFriendsFramsAsync(jwt, userId);

            return View(new FarmViewModel { FarmOverview = farmDto, FriendsFarms = farms });
        }

        [HttpGet]
        public async Task<IActionResult> FarmDetails(Guid farmId)
        {
            var (jwt, userId) = GetRequiredParameters();

            var farmDetails = await _farmService.GetFarmDetailsAsync(jwt, userId, farmId);
            var farmDetailVM = new FarmDetailsViewModel();
            if (farmDetails.Id.ToString() != Request.Cookies["my-farm-id"])
            {
                farmDetailVM.FarmDetails = farmDetails;
                return View(farmDetailVM);
            }

            var farmStatistics = await _farmService.GetFarmStatisticsAsync(jwt, userId, farmId);

            farmDetailVM.FarmDetails = farmDetails;
            farmDetailVM.FarmForUpdate = _mapper.Map<FarmForUpdateDto>(farmDetails);
            farmDetailVM.FarmStatistics = farmStatistics;

            return View(farmDetailVM);
        }

        [HttpPost]
        public async Task<IActionResult> Feed(Guid farmId, Guid petId)
        {
            var (jwt, userId) = GetRequiredParameters();

            await _petService.FeedAsync(jwt, userId, farmId, petId);

            return RedirectToAction("FarmDetails", new { farmId });
        }

        [HttpPost]
        public async Task<IActionResult> GiveADrink(Guid farmId, Guid petId)
        {
            var (jwt, userId) = GetRequiredParameters();

            await _petService.GiveADrinkAsync(jwt, userId, farmId, petId);

            return RedirectToAction("FarmDetails", new { farmId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateFarm(FarmViewModel farmVM)
        {
            var (jwt, userId) = GetRequiredParameters();

            var farmDto = await _farmService.CreateFarmAsync(jwt, userId, farmVM.FarmForCreation);

            return RedirectToAction("FarmOverview");
        }

        [HttpPost]
        public async Task<IActionResult> InviteFriend(Guid farmId, FarmDetailsViewModel farmVM)
        {
            var (jwt, userId) = GetRequiredParameters();

            await _farmService.InviteFriendAsync(jwt, userId, farmId, farmVM.UserForInviting);

            return RedirectToAction("FarmDetails", new { farmId });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFarm(Guid farmId, FarmDetailsViewModel farmVM)
        {
            var (jwt, userId) = GetRequiredParameters();

            await _farmService.UpdateFarmAsync(jwt, userId, farmId, farmVM.FarmForUpdate);

            return RedirectToAction("FarmDetails", new { farmId });
        }

        [HttpPost]
        public async Task<IActionResult> CreatePet(Guid farmId, FarmDetailsViewModel farmVM)
        {
            var (jwt, userId) = GetRequiredParameters();

            await _petService.CreatePetAsync(jwt, userId, farmId, farmVM.PetForCreation);

            return RedirectToAction("FarmDetails", new { farmId });
        }
    }
}
