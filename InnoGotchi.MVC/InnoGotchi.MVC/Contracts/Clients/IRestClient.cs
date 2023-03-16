namespace InnoGotchi.MVC.Contracts.Clients
{
    public interface IRestClient<TResource> where TResource : class
    {
        Task<(IEnumerable<TResource>, string)> GetAllAsync(string actionName = "", string jwt = " ");
        Task<T> GetAsync<T>(string id, string actionName = "", string jwt = "") where T : class;
        Task<TResource> PostAsync<T>(string id, T resource, string actionName = "", string jwt = "");
        Task PutAsync<T>(string id, T resource, string actionName = "", string jwt = "");
    }
}
