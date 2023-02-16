using InnoGotchi.MVC.Models;
using InnoGotchi.MVC.Models.User;

namespace InnoGotchi.MVC.Contracts.Services
{
    public interface IAuthenticationService
    {
        Task<UserForAuthenticationDto> RegisterUserAsync(UserForRegistrationDto userForReg);
        Task<AccessTokenDto> SignInAsync(UserForAuthenticationDto userForAuth);
        Task<UserInfoForLayoutDto> GetUserForLayoutAsync(string jwt, string id);
    }
}
