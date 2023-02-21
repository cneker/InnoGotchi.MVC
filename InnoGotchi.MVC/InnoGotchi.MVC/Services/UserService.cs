using InnoGotchi.MVC.Contracts.Helpers;
using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.Models.User;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace InnoGotchi.MVC.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConvertHelper _convertHelper;

        public UserService(IHttpClientFactory httpClientFactory, IConvertHelper convertHelper)
        {
            _httpClientFactory = httpClientFactory;
            _convertHelper = convertHelper;
        }

        public async Task<UserInfoDto> GetUserInfoDtoAsync(string jwt, string id)
        {
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://localhost:7208/api/users/{id}"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json; charset=utf-8" },
                    { HeaderNames.Authorization, $"Bearer {jwt}" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception("Something went wrong");
            }
            var jsonContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var userDto = JsonConvert.DeserializeObject<UserInfoDto>(jsonContent);
            return userDto;
        }

        public async Task UpdateUserInfoAsync(string jwt, string id, UserInfoForUpdateDto userDto)
        {
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7208/api/users/{id}"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json; charset=utf-8" },
                    { HeaderNames.Authorization, $"Bearer {jwt}" }
                },
                Content = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8, "application/json")
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception("Something went wrong");
            }
        }

        public async Task ChangePasswordAsync(string jwt, string id, PasswordChangingDto passwordDto)
        {
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7208/api/users/{id}/change-password"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json; charset=utf-8" },
                    { HeaderNames.Authorization, $"Bearer {jwt}" }
                },
                Content = new StringContent(JsonConvert.SerializeObject(passwordDto), Encoding.UTF8, "application/json")
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception("Something went wrong");
            }
        }

        public async Task UpdateAvatarAsync(string jwt, string id, IFormFile file)
        {
            var base64Image = _convertHelper.ConvertFromImageToBase64(file);
            var avatarDto = new AvatarChangingDto
            {
                Base64Image = base64Image,
                FileName = file.FileName
            };
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7208/api/users/{id}/update-avatar"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json; charset=utf-8" },
                    { HeaderNames.Authorization, $"Bearer {jwt}" }
                },
                Content = new StringContent(JsonConvert.SerializeObject(avatarDto), Encoding.UTF8, "application/json")
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception("Something went wrong");
            }
        }
    }
}
