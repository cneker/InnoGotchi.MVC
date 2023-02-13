using InnoGotchi.MVC.Models.Farm;
using InnoGotchi.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;

namespace InnoGotchi.MVC.Controllers
{
    public class FarmController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public FarmController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> FarmOverview()
        {
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://localhost:7208/api/farms/my-farm"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {Request.Cookies["jwt"]}" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                    return RedirectToAction("SignIn", "Authentication");
                return View("~/Views/Shared/Error.cshtml", httpResponseMessage.StatusCode.ToString());
            }

            var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
            var farmDto = JsonConvert.DeserializeObject<FarmOverviewDto>(contentStream);


            httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://localhost:7208/api/farms/friends"),
                Headers =
                {
                    { HeaderNames.Accept, "application/json" },
                    { HeaderNames.Authorization, $"Bearer {Request.Cookies["jwt"]}" }
                }
            };

            httpClient = _httpClientFactory.CreateClient();
            httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                if (httpResponseMessage.StatusCode == HttpStatusCode.Unauthorized)
                    return RedirectToAction("SignIn", "Authentication");
                return View("~/Views/Shared/Error.cshtml", httpResponseMessage.StatusCode.ToString());
            }

            contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
            var farms = JsonConvert.DeserializeObject<List<FarmOverviewDto>>(contentStream);

            //получить пользователя?

            return View(new FarmViewModel { FarmOverview = farmDto, FriendsFarms = farms });
        }

        public IActionResult FarmDetails()
        {
            return View();
        }

        public IActionResult FarmStatistics()
        {
            return View();
        }
    }
}
