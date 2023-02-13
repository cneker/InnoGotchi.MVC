using AutoMapper;
using InnoGotchi.Application.DataTransferObjects;
using InnoGotchi.MVC.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;


namespace InnoGotchi.MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMapper _mapper;
        public AuthenticationController(IHttpClientFactory httpClientFactory, IMapper mapper)
        {
            _httpClientFactory = httpClientFactory;
            _mapper = mapper;
        }

        public IActionResult SignIn()
        {
            return View(new UserForAuthenticationDto());
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(UserForRegistrationDto userForReg)
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

            if (!httpResponseMessage.IsSuccessStatusCode)
                return View("~/Views/Shared/Error.cshtml", httpResponseMessage.StatusCode.ToString());

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var userEmail = new StreamReader(contentStream).ReadLine();

            return View("~/Views/Authentication/SignIn.cshtml", new UserForAuthenticationDto { Email = userEmail });
        }

        [HttpPost]  
        public async Task<IActionResult> SignIn(UserForAuthenticationDto userForAuth)
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

            if (!httpResponseMessage.IsSuccessStatusCode)
                return View("~/Views/Shared/Error.cshtml", httpResponseMessage.StatusCode.ToString());

            var jsonContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var token = JsonConvert.DeserializeObject<AccessTokenDto>(jsonContent);

            Response.Cookies.Append("jwt", token.AccessToken);

            return RedirectToAction("FarmOverview", "Farm");
        }

        public async Task<IActionResult> LogOut()
        {
            Response.Cookies.Delete("jwt");

            return RedirectToAction("Index", "Home");
        }
    }
}
