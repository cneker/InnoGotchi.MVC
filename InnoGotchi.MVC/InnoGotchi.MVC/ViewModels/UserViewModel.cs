using InnoGotchi.MVC.Models.User;

namespace InnoGotchi.MVC.ViewModels
{
    public class UserViewModel
    {
        public UserInfoDto UserInfo { get; set; }
        public UserInfoForUpdateDto UserInfoForUpdate { get; set; }
        public PasswordChangingDto PasswordChanging { get; set; }
        public IFormFile Avatar { get; set; }

        public UserViewModel()
        {
            PasswordChanging= new PasswordChangingDto();
        }
    }
}
