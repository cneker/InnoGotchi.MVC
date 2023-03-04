using InnoGotchi.MVC.Models.User;
using System.Diagnostics;

namespace InnoGotchi.MVC.Contracts.Clients
{
    public interface IUserClient
    {
        Task<UserInfoDto> GetUserAsync(string id, string jwt);
        Task<UserInfoDto> CreateUserAsync(UserForRegistrationDto userForReg);
        Task UpdateUserInfoAsync(string id, string jwt, UserInfoForUpdateDto userDto);
        Task ChangePasswordAsync(string id, string jwt, PasswordChangingDto passwordDto);
        Task UpdateAvatarAsync(string id, string jwt, AvatarChangingDto avatarDto);
    }
}
