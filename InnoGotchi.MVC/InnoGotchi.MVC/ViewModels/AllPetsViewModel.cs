using InnoGotchi.MVC.Models.Pet;
using InnoGotchi.MVC.RequestFeatures;

namespace InnoGotchi.MVC.ViewModels
{
    public class AllPetsViewModel
    {
        public RequestParameters RequestParameters { get; set; }
        public PetPagingDto PetPaging { get; set; }
    }
}
