using InnoGotchi.MVC.Models.Farm;
using InnoGotchi.MVC.Models.User;

namespace InnoGotchi.MVC.Contracts.Services
{
    public interface IFarmService
    {
        Task<FarmOverviewDto> GetUserFarmOverviewAsync(string jwt, string userId);
        Task<IEnumerable<FarmOverviewDto>> GetFriendsFramsAsync(string jwt, string userId);
        Task<FarmOverviewDto> CreateFarmAsync(string jwt, string userId, FarmForCreationDto farmDto);
        Task<FarmStatisticsDto> GetFarmStatisticsAsync(string jwt, string userId, Guid farmId);
        Task<FarmDetailsDto> GetFarmDetailsAsync(string jwt, string userId, Guid farmId);
        Task InviteFriendAsync(string jwt, string userId, Guid farmId, UserForInvitingDto userDto);
        Task UpdateFarmAsync(string jwt, string userId, Guid farmId, FarmForUpdateDto farmDto);
    }
}
