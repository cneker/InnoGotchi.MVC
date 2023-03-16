using InnoGotchi.MVC.Models.Farm;
using InnoGotchi.MVC.Models.User;

namespace InnoGotchi.MVC.Contracts.Clients
{
    public interface IFarmClient
    {
        Task<IEnumerable<FarmOverviewDto>> GetFriendsFarmsAsync(string userId, string jwt);
        Task<FarmOverviewDto> GetFarmAsync(string userId, string jwt);
        Task<FarmOverviewDto> CreateFarmAsync(string userId, string jwt, FarmForCreationDto farmDto);
        Task<FarmStatisticsDto> GetFarmStatisticsAsync(string userId, string farmId, string jwt);
        Task<FarmDetailsDto> GetFarmDetailsAsync(string userId, string farmId, string jwt);
        Task InviteFriendAsync(string userId, string farmId, string jwt, UserForInvitingDto userDto);
        Task UpdateFarmAsync(string userId, string farmId, string jwt, FarmForUpdateDto farmDto);
    }
}
