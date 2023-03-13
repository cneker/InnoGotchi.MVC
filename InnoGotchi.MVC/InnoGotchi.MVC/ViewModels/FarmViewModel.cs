using InnoGotchi.MVC.Models.Farm;

namespace InnoGotchi.MVC.ViewModels
{
    public class FarmViewModel
    {
        public FarmOverviewDto FarmOverview { get; set; }
        public IEnumerable<FarmOverviewDto> FriendsFarms { get; set; } = Enumerable.Empty<FarmOverviewDto>();
        public FarmForCreationDto FarmForCreation { get; set; }
    }
}
