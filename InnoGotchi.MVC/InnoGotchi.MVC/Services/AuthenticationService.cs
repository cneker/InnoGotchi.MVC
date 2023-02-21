using AutoMapper;
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
        private readonly IMapper _mapper;

        public AuthenticationService(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
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

            var jsonContent = await httpResponseMessage.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<UserInfoDto>(jsonContent);

            var userForAuth = _mapper.Map<UserForAuthenticationDto>(user);
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
