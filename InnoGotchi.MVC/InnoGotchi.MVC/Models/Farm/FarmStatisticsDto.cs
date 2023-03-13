namespace InnoGotchi.MVC.Models.Farm
{
    public class FarmStatisticsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int AlivePetsCount { get; set; }
        public int DeadPetsCount { get; set; }
        public double AverageFeedingPeriod { get; set; }
        public double AverageThirstQuenchingPeriod { get; set; }
        public double AveragePetsHappinessDaysCount { get; set; }
        public int AveragePetsAge { get; set; }
    }
}
