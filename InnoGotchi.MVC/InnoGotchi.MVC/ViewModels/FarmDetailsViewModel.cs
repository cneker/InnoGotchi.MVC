using InnoGotchi.MVC.Models.Farm;
using InnoGotchi.MVC.Models.Pet;
using InnoGotchi.MVC.Models.User;

namespace InnoGotchi.MVC.ViewModels
{
    public class FarmDetailsViewModel
    {
        public FarmDetailsDto FarmDetails { get; set; }
        public UserForInvitingDto UserForInviting { get; set; }
        public FarmForUpdateDto FarmForUpdate { get; set; }
        public PetForCreationDto PetForCreation { get; set; }
        public FarmStatisticsDto FarmStatistics { get; set; }
        public FarmDetailsViewModel()
        {
            UserForInviting= new UserForInvitingDto();
            PetForCreation = new PetForCreationDto();
        }
    }
}
