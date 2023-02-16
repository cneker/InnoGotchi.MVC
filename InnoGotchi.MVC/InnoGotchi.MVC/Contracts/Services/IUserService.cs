using InnoGotchi.MVC.Models.User;

namespace InnoGotchi.MVC.Contracts.Services
{
    public interface IUserService
    {
        Task<UserInfoDto> GetUserInfoDtoAsync(string jwt, string id);
        Task UpdateUserInfoAsync(string jwt, string id, UserInfoForUpdateDto userDto);
        Task ChangePasswordAsync(string jwt, string id, PasswordChangingDto passwordDto);
        Task UpdateAvatarAsync(string jwt, string id, IFormFile file);
    }
}
