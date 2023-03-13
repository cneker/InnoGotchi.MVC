using InnoGotchi.MVC.Models;
using InnoGotchi.MVC.Models.User;

namespace InnoGotchi.MVC.Contracts.Clients
{
    public interface IAuthenticationClient
    {
        Task<AccessTokenDto> SignInAsync(UserForAuthenticationDto userForAuth);
    }
}
