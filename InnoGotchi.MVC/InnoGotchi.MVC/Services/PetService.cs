using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.Models.Pet;
using InnoGotchi.MVC.RequestFeatures;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace InnoGotchi.MVC.Services
{
    public class PetService : IPetService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PetService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task FeedAsync(string jwt, string userId, Guid farmId, Guid petId)
        {
            var httpRequstMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7208/api/users/{userId}/farm/{farmId}/pets/{petId}/feed"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwt}" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequstMessage);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
                    throw new Exception("You are not the owner or collaborator of this farm");
                else
                    throw new Exception("Something went wrong");
            }
        }

        public async Task<PetDetailsDto> GetPetDetailsAsync(string jwt, string userId, Guid farmId, Guid petId)
        {
            var httpRequstMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://localhost:7208/api/users/{userId}/farm/{farmId}/pets/{petId}"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwt}" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequstMessage);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
                    throw new Exception("You are not the owner or collaborator of this farm");
                else
                    throw new Exception("Something went wrong");
            }
            var jsonContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var farm = JsonConvert.DeserializeObject<PetDetailsDto>(jsonContent);

            return farm;
        }

        public async Task GiveADrinkAsync(string jwt, string userId, Guid farmId, Guid petId)
        {
            var httpRequstMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7208/api/users/{userId}/farm/{farmId}/pets/{petId}/give-a-drink"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwt}" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequstMessage);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.Forbidden)
                    throw new Exception("You are not the owner or collaborator of this farm");
                else
                    throw new Exception("Something went wrong");
            }
        }

        public async Task UpdatePetAsync(string jwt, string userId, Guid farmId, Guid petId, PetForUpdateDto petDto)
        {
            var httpRequstMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7208/api/users/{userId}/farm/{farmId}/pets/{petId}"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwt}" }
                },
                Content = new StringContent(JsonConvert.SerializeObject(petDto), Encoding.UTF8, "application/json")
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequstMessage);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception("Something went wrong");
            }
        }

        public async Task<PetPagingDto> GetPetsAsync(string jwt, int pageNumber, string orderBy, int pageSize)
        {
            var httpRequstMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://localhost:7208/api/pets?OrderBy={orderBy}&PageSize={pageSize}&PageNumber={pageNumber}"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwt}" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequstMessage);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception("Something went wrong");
            }
            var jsonContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var pets = JsonConvert.DeserializeObject<IEnumerable<PetDetailsDto>>(jsonContent);

            var paginationHeaderValue = httpResponseMessage.Headers.GetValues("X-Pagination").First();

            var petPaging = new PetPagingDto
            {
                PetDetails = pets,
                MetaData = JsonConvert.DeserializeObject<MetaData>(paginationHeaderValue)
            };

            return petPaging;
        }

        public async Task CreatePetAsync(string jwt, string userId, Guid farmId, PetForCreationDto petDto)
        {
            var httpRequstMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"https://localhost:7208/api/users/{userId}/farm/{farmId}/pets"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {jwt}" }
                },
                Content = new StringContent(JsonConvert.SerializeObject(petDto), Encoding.UTF8, "application/json")
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequstMessage);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.InternalServerError)
                    throw new Exception("Something went wrong");
            }
        }
    }
}
