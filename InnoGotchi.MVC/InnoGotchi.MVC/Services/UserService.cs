using InnoGotchi.MVC.Contracts.Clients;
using InnoGotchi.MVC.Contracts.Helpers;
using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.Models.User;

namespace InnoGotchi.MVC.Services
{
    public class UserService : IUserService
    {
        private readonly IConvertHelper _convertHelper;
        private readonly IUserClient _userClient;

        public UserService(IConvertHelper convertHelper, IUserClient userClient)
        {
            _convertHelper = convertHelper;
            _userClient = userClient;
        }

        public async Task<UserInfoDto> GetUserInfoDtoAsync(string jwt, string id)
        {
            return await _userClient.GetUserAsync(id, jwt);
        }

        public async Task UpdateUserInfoAsync(string jwt, string id, UserInfoForUpdateDto userDto)
        {
            await _userClient.UpdateUserInfoAsync(id, jwt, userDto);
        }

        public async Task ChangePasswordAsync(string jwt, string id, PasswordChangingDto passwordDto)
        {
            await _userClient.ChangePasswordAsync(id, jwt, passwordDto);
        }

        public async Task UpdateAvatarAsync(string jwt, string id, IFormFile file)
        {
            var base64Image = _convertHelper.ConvertFromImageToBase64(file);
            var avatarDto = new AvatarChangingDto
            {
                Base64Image = base64Image,
                FileName = file.FileName
            };
            await _userClient.UpdateAvatarAsync(id, jwt, avatarDto);
        }
    }
}
