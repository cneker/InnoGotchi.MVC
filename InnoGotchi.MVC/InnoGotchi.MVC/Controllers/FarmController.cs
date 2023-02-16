using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.MVC.Controllers
{
    public class FarmController : Controller
    {
        private readonly IFarmService _farmService;
        public FarmController(IFarmService farmService)
        {
            _farmService = farmService;
        }

        public async Task<IActionResult> FarmOverview()
        {
            var jwt = Request.Cookies["jwt"];
            var userId = User.Claims.First(c => c.Type == "Id").Value;

            var farmDto = await _farmService.GetUserFarmOverviewAsync(jwt, userId);

            Response.Cookies.Append("my-farm-id", farmDto.Id.ToString());

            var farms = await _farmService.GetFriendsFramsAsync(jwt, userId);

            return View(new FarmViewModel { FarmOverview = farmDto, FriendsFarms = farms });
        }

        public async Task<IActionResult> FarmDetails(Guid farmId)
        {
            var jwt = Request.Cookies["jwt"];
            var userId = User.Claims.First(c => c.Type == "Id").Value;

            var farmDetails = await _farmService.GetFarmDetailsAsync(jwt, userId, farmId.ToString());

            return View(new FarmDetailsViewModel() { FarmDetails = farmDetails});
        }

        public async Task<IActionResult> FarmStatistics(Guid farmId)
        {
            var jwt = Request.Cookies["jwt"];
            var userId = User.Claims.First(c => c.Type == "Id").Value;

            var farmStatistics = await _farmService.GetFarmStatisticsAsync(jwt, userId, farmId.ToString());

            return View(farmStatistics);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFarm(FarmViewModel farmVM)
        {
            var jwt = Request.Cookies["jwt"];
            var userId = User.Claims.First(c => c.Type == "Id").Value;

            var farmDto = await _farmService.CreateFarm(jwt, userId, farmVM.FarmForCreation);

            return RedirectToAction("FarmOverview");
        }

        [HttpPost]
        public async Task<IActionResult> InviteFriend(FarmDetailsViewModel farmVM)
        {
            var jwt = Request.Cookies["jwt"];
            var userId = User.Claims.First(c => c.Type == "Id").Value;

            await _farmService.InviteFriendAsync(jwt, userId, farmVM.UserForInviting);

            return RedirectToAction("FarmDetails");
        }
    }
}
