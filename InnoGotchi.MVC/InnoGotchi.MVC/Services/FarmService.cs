using InnoGotchi.MVC.Contracts.Clients;
using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.Models.Farm;
using InnoGotchi.MVC.Models.User;

namespace InnoGotchi.MVC.Services
{
    public class FarmService : IFarmService
    {
        private readonly IFarmClient _farmClient;

        public FarmService(IFarmClient farmClient)
        {
            _farmClient = farmClient;
        }

        public async Task<IEnumerable<FarmOverviewDto>> GetFriendsFramsAsync(string jwt, string userId)
        {
            return await _farmClient.GetFriendsFarmsAsync(userId, jwt);
        }

        public async Task<FarmOverviewDto> GetUserFarmOverviewAsync(string jwt, string userId)
        {
            return await _farmClient.GetFarmAsync(userId, jwt);
        }

        public async Task<FarmOverviewDto> CreateFarmAsync(string jwt, string userId, FarmForCreationDto farmDto)
        {
            return await _farmClient.CreateFarmAsync(userId, jwt, farmDto);
        }

        public async Task<FarmStatisticsDto> GetFarmStatisticsAsync(string jwt, string userId, Guid farmId)
        {
            return await _farmClient.GetFarmStatisticsAsync(userId, farmId.ToString(), jwt);
        }

        public async Task<FarmDetailsDto> GetFarmDetailsAsync(string jwt, string userId, Guid farmId)
        {
            return await _farmClient.GetFarmDetailsAsync(userId, farmId.ToString(), jwt);
        }

        public async Task InviteFriendAsync(string jwt, string userId, Guid farmId, UserForInvitingDto userDto)
        {
            await _farmClient.InviteFriendAsync(userId, farmId.ToString(), jwt, userDto);
        }

        public async Task UpdateFarmAsync(string jwt, string userId, Guid farmId, FarmForUpdateDto farmDto)
        {
            await _farmClient.UpdateFarmAsync(userId, farmId.ToString(), jwt, farmDto);
        }
    }
}
