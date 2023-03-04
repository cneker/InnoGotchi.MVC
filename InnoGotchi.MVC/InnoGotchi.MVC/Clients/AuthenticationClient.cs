using InnoGotchi.MVC.Contracts.Clients;
using InnoGotchi.MVC.Models;
using InnoGotchi.MVC.Models.User;

namespace InnoGotchi.MVC.Clients
{
    public class AuthenticationClient : RestClient<AccessTokenDto>, IAuthenticationClient
    {
        public AuthenticationClient(IHttpClientFactory clientFactory, IConfiguration configuration)
            : base(clientFactory, configuration, "authentication")
        {
        }

        public async Task<AccessTokenDto> SignInAsync(UserForAuthenticationDto userForAuth)
        {
            return await PostAsync("", userForAuth);
        }
    }
}
