using InnoGotchi.MVC.Contracts.Clients;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace InnoGotchi.MVC.Clients
{
    public abstract class RestClient<TResource> : IRestClient<TResource> where TResource : class
    {
        private readonly HttpClient _client;
        public StringBuilder AddressSuffix;
        public RestClient(IHttpClientFactory clientFactory, IConfiguration configuration, string addressSuffix)
        {
            var webAPISettings = configuration.GetSection("WebAPIHttpClient");
            var name = webAPISettings.GetRequiredSection("Name").Value;
            _client = clientFactory.CreateClient(name);
            AddressSuffix = new StringBuilder(addressSuffix);
        }

        private void SetAuthenticationHeader(string jwt)
        {
            if (jwt != "")
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", jwt);
        }

        public async Task<(IEnumerable<TResource>, string)> GetAllAsync(string actionName = "", string jwt = "")
        {
            SetAuthenticationHeader(jwt);

            var httpResponseMessage = await _client.GetAsync(AddressSuffix + actionName);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Something went wrong");
            }
            string paginationHeaderValue = string.Empty;
            if (AddressSuffix.Equals("pets"))
                paginationHeaderValue = httpResponseMessage.Headers.GetValues("X-Pagination").First();
            var values = await httpResponseMessage.Content.ReadAsAsync<IEnumerable<TResource>>();
            return (values, paginationHeaderValue);
        }

        public async Task<T> GetAsync<T>(string id, string actionName = "", string jwt = "") where T : class
        {
            SetAuthenticationHeader(jwt);

            var httpResponseMessage = await _client.GetAsync(AddressSuffix + id + actionName);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                switch (httpResponseMessage.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return null;
                    case HttpStatusCode.Forbidden:
                        throw new Exception("You don't have any permissions do to this action");
                    default:
                        throw new Exception("Something went wrong");
                }   
            }

            return await httpResponseMessage.Content.ReadAsAsync<T>();
        }

        public async Task<TResource> PostAsync<T>(string id, T resource, string actionName = "", string jwt = "")
        {
            SetAuthenticationHeader(jwt);

            var httpContent = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");
            var httpResponseMessage = await _client.PostAsync(AddressSuffix + id + actionName, httpContent);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                switch (httpResponseMessage.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        return null;
                    case HttpStatusCode.NotFound:
                        return null;
                    default:
                        throw new Exception("Something went wrong");
                }
            }

            return await httpResponseMessage.Content.ReadAsAsync<TResource>();
        }

        public async Task PutAsync<T>(string id, T resource, string actionName = "", string jwt = "")
        {
            SetAuthenticationHeader(jwt);

            var httpContent = new StringContent(JsonConvert.SerializeObject(resource), Encoding.UTF8, "application/json");
            var httpResponseMessage = await _client.PutAsync(AddressSuffix + id + actionName, httpContent);
            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                switch (httpResponseMessage.StatusCode)
                {
                    case HttpStatusCode.Forbidden:
                        throw new Exception("You don't have any permissions do to this action");
                    case HttpStatusCode.BadRequest:
                        return;
                    default:
                        throw new Exception("Something went wrong");
                }
            }
        }
    }
}
