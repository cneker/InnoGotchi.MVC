using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.Models;
using InnoGotchi.MVC.Models.User;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace InnoGotchi.MVC.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthenticationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UserInfoForLayoutDto> GetUserForLayoutAsync(string jwt, string id)
        {
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://localhost:7208/api/users/{id}/layout"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json; charset=utf-8" },
                    { HeaderNames.Authorization, $"Bearer {jwt}" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            var jsonContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var userForLayout = JsonConvert.DeserializeObject<UserInfoForLayoutDto>(jsonContent);
            return userForLayout;
        }

        public async Task<UserForAuthenticationDto> RegisterUserAsync(UserForRegistrationDto userForReg)
        {
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7208/api/users"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json" }
                },
                Content = new StringContent(
                    JsonConvert.SerializeObject(userForReg), Encoding.UTF8, "application/json")
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var userEmail = new StreamReader(contentStream).ReadLine();

            var userForAuth = new UserForAuthenticationDto { Email = userEmail };
            return userForAuth;
        }

        public async Task<AccessTokenDto> SignInAsync(UserForAuthenticationDto userForAuth)
        {
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7208/api/authentication"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json; charset=utf-8" }
                },
                Content = new StringContent(
                    JsonConvert.SerializeObject(userForAuth), Encoding.UTF8, "application/json")
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            var jsonContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var token = JsonConvert.DeserializeObject<AccessTokenDto>(jsonContent);
            return token;
        }
    }
}
