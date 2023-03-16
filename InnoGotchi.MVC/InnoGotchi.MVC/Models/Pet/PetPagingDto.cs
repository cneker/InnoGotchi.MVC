using InnoGotchi.MVC.RequestFeatures;

namespace InnoGotchi.MVC.Models.Pet
{
    public class PetPagingDto
    {
        public IEnumerable<PetDetailsDto> PetDetails { get; set; }
        public MetaData MetaData { get; set; }
    }
}
