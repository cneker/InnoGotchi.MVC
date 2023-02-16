using AutoMapper;
using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.Models.User;
using InnoGotchi.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace InnoGotchi.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> UserProfile()
        {
            var jwt = Request.Cookies["jwt"];
            var id = User.Claims.First(x => x.Type == "Id").Value;

            var userDto = await _userService.GetUserInfoDtoAsync(jwt, id);

            var userViewModel = new UserViewModel()
            { 
                UserInfo = userDto,
                UserInfoForUpdate = _mapper.Map<UserInfoForUpdateDto>(userDto)
            };

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserInfo(UserViewModel userVM)
        {
            var jwt = Request.Cookies["jwt"];
            var id = User.Claims.First(x => x.Type == "Id").Value;

            await _userService.UpdateUserInfoAsync(jwt, id, userVM.UserInfoForUpdate);

            Response.Cookies.Append("name", userVM.UserInfoForUpdate.FirstName);

            return RedirectToAction("UserProfile");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserViewModel userVM)
        {
            var jwt = Request.Cookies["jwt"];
            var id = User.Claims.First(x => x.Type == "Id").Value;

            await _userService.ChangePasswordAsync(jwt, id, userVM.PasswordChanging);

            return RedirectToAction("UserProfile");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAvatar(UserViewModel userVM)
        {
            var jwt = Request.Cookies["jwt"];
            var id = User.Claims.First(x => x.Type == "Id").Value;

            await _userService.UpdateAvatarAsync(jwt, id, userVM.Avatar);
            var path = User.Claims.First(c => c.Type == "Id").Value + "-" + userVM.Avatar.FileName;
            Response.Cookies.Append("avatar", path);

            return RedirectToAction("UserProfile");
        }
    }
}
