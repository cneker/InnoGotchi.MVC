using InnoGotchi.MVC.Models.Pet;

namespace InnoGotchi.MVC.Models.Farm
{
    public class FarmDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PetsCount { get; set; }

        public IEnumerable<PetOverviewDto> Pets { get; set; }
    }
}
