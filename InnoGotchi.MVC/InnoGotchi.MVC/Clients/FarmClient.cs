using InnoGotchi.MVC.Contracts.Clients;
using InnoGotchi.MVC.Models.Farm;
using InnoGotchi.MVC.Models.User;

namespace InnoGotchi.MVC.Clients
{
    public class FarmClient : RestClient<FarmOverviewDto>, IFarmClient
    {
        public FarmClient(IHttpClientFactory clientFactory, IConfiguration configuration)
            : base(clientFactory, configuration, "users/userId/farm/")
        {
        }

        private void SetAddressSuffix(string userId)
        {
            AddressSuffix.Replace("userId", userId);
        }

        public async Task<IEnumerable<FarmOverviewDto>> GetFriendsFarmsAsync(string userId, string jwt)
        {
            SetAddressSuffix(userId);
            return (await GetAllAsync(jwt: jwt, actionName: "friends")).Item1;
        }

        public async Task<FarmOverviewDto> GetFarmAsync(string userId, string jwt)
        {
            SetAddressSuffix(userId);
            return await GetAsync<FarmOverviewDto>("", jwt: jwt);
        }

        public async Task<FarmOverviewDto> CreateFarmAsync(string userId, string jwt, FarmForCreationDto farmDto)
        {
            SetAddressSuffix(userId);
            return await PostAsync("", farmDto, jwt: jwt);
        }

        public async Task<FarmStatisticsDto> GetFarmStatisticsAsync(string userId, string farmId, string jwt)
        {
            SetAddressSuffix(userId);
            return await GetAsync<FarmStatisticsDto>(farmId, "/statistics", jwt);
        }

        public async Task<FarmDetailsDto> GetFarmDetailsAsync(string userId, string farmId, string jwt)
        {
            SetAddressSuffix(userId);
            return await GetAsync<FarmDetailsDto>(farmId, "/detail", jwt);
        }

        public async Task InviteFriendAsync(string userId, string farmId, string jwt, UserForInvitingDto userDto)
        {
            SetAddressSuffix(userId);
            await PostAsync(farmId, userDto, "/invite-collaborator", jwt);
        }

        public async Task UpdateFarmAsync(string userId, string farmId, string jwt, FarmForUpdateDto farmDto)
        {
            SetAddressSuffix(userId);
            await PutAsync(farmId, farmDto, jwt: jwt);
        }
    }
}
