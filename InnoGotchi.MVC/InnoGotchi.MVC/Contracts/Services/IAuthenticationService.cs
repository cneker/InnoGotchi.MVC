using InnoGotchi.MVC.Models;
using InnoGotchi.MVC.Models.User;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InnoGotchi.MVC.Contracts.Services
{
    public interface IAuthenticationService
    {
        Task<UserForAuthenticationDto> RegisterUserAsync(UserForRegistrationDto userForReg, ModelStateDictionary modelState);
        Task<AccessTokenDto> SignInAsync(UserForAuthenticationDto userForAuth, ModelStateDictionary modelState);
    }
}
