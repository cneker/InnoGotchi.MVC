using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.Models.Farm;
using InnoGotchi.MVC.Models.User;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace InnoGotchi.MVC.Services
{
    public class FarmService : IFarmService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public FarmService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<FarmOverviewDto>> GetFriendsFramsAsync(string jwt, string userId)
        {
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://localhost:7208/api/users/{userId}/farm/friends"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json; charset=utf-8" },
                    { HeaderNames.Authorization, $"Bearer {jwt}" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            var jsonContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var freindsFarms = JsonConvert.DeserializeObject<IEnumerable<FarmOverviewDto>>(jsonContent);

            return freindsFarms;
        }

        public async Task<FarmOverviewDto> GetUserFarmOverviewAsync(string jwt, string userId)
        {
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://localhost:7208/api/users/{userId}/farm"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json; charset=utf-8" },
                    { HeaderNames.Authorization, $"Bearer {jwt}" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            var jsonContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var farmOverview = JsonConvert.DeserializeObject<FarmOverviewDto>(jsonContent);

            return farmOverview;
        }

        public async Task<FarmOverviewDto> CreateFarm(string jwt, string userId, FarmForCreationDto farmDto)
        {
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://localhost:7208/api/users/{userId}/farm"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json; charset=utf-8" },
                    { HeaderNames.Authorization, $"Bearer {jwt}"}
                },
                Content = new StringContent(JsonConvert.SerializeObject(farmDto), Encoding.UTF8, "application/json")
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            var jsonContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var farmOverview = JsonConvert.DeserializeObject<FarmOverviewDto>(jsonContent);

            return farmOverview;
        }

        public async Task<FarmStatisticsDto> GetFarmStatisticsAsync(string jwt, string userId, string farmId)
        {
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://localhost:7208/api/users/{userId}/farm/{farmId}/statistics"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json; charset=utf-8" },
                    { HeaderNames.Authorization, $"Bearer {jwt}" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            var jsonContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var farmStatistics = JsonConvert.DeserializeObject<FarmStatisticsDto>(jsonContent);

            return farmStatistics;
        }

        public async Task<FarmDetailsDto> GetFarmDetailsAsync(string jwt, string userId, string farmId)
        {
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://localhost:7208/api/users/{userId}/farm/{farmId}/detail"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json; charset=utf-8" },
                    { HeaderNames.Authorization, $"Bearer {jwt}" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            var jsonContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var farmDetails = JsonConvert.DeserializeObject<FarmDetailsDto>(jsonContent);

            return farmDetails;
        }

        public async Task InviteFriendAsync(string jwt, string userId, string farmId, UserForInvitingDto userDto)
        {
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://localhost:7208/api/users/{userId}/farm/{farmId}/invite-collaborator"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json; charset=utf-8" },
                    { HeaderNames.Authorization, $"Bearer {jwt}" }
                },
                Content = new StringContent(JsonConvert.SerializeObject(userDto), Encoding.UTF8, "application/json")
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            await httpResponseMessage.Content.ReadAsStringAsync();
        }

        public async Task UpdateFarmAsync(string jwt, string userId, string farmId, FarmForUpdateDto farmDto)
        {
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7208/api/users/{userId}/farm/{farmId}"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json; charset=utf-8" },
                    { HeaderNames.Authorization, $"Bearer {jwt}" }
                },
                Content = new StringContent(JsonConvert.SerializeObject(farmDto), Encoding.UTF8, "application/json")
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            await httpResponseMessage.Content.ReadAsStringAsync();
        }
    }
}
