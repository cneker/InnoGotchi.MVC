using InnoGotchi.MVC.Models.Farm;
using InnoGotchi.MVC.Models.User;

namespace InnoGotchi.MVC.ViewModels
{
    public class FarmDetailsViewModel
    {
        public FarmDetailsDto FarmDetails { get; set; }
        public UserForInvitingDto UserForInviting { get; set; }
        public FarmDetailsViewModel()
        {
            UserForInviting= new UserForInvitingDto();
        }
    }
}
