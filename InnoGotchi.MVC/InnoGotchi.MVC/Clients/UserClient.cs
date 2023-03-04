using InnoGotchi.MVC.Contracts.Clients;
using InnoGotchi.MVC.Models.User;

namespace InnoGotchi.MVC.Clients
{
    public class UserClient : RestClient<UserInfoDto>, IUserClient
    {
        public UserClient(IHttpClientFactory clientFactory, IConfiguration configuration) 
            : base(clientFactory, configuration, "users/")
        {
        }

        public async Task<UserInfoDto> GetUserAsync(string id, string jwt)
        {
            return await GetAsync<UserInfoDto>(id, jwt: jwt);
        }

        public async Task<UserInfoDto> CreateUserAsync(UserForRegistrationDto userForReg)
        {
            return await PostAsync("", userForReg);
        }

        public async Task UpdateUserInfoAsync(string id, string jwt, UserInfoForUpdateDto userDto)
        {
            await PutAsync(id, userDto, jwt: jwt);
        }

        public async Task ChangePasswordAsync(string id, string jwt, PasswordChangingDto passwordDto)
        {
            await PutAsync(id, passwordDto, jwt: jwt, actionName: "/change-password");
        }

        public async Task UpdateAvatarAsync(string id, string jwt, AvatarChangingDto avatarDto)
        {
            await PutAsync(id, avatarDto, jwt: jwt, actionName: "/update-avatar");
        }
    }
}
