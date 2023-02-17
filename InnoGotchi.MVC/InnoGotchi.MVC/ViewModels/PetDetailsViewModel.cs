using InnoGotchi.MVC.Models.Pet;

namespace InnoGotchi.MVC.ViewModels
{
    public class PetDetailsViewModel
    {
        public PetDetailsDto PetDetails { get; set; }
        public Guid FarmId { get; set; }
    }
}
