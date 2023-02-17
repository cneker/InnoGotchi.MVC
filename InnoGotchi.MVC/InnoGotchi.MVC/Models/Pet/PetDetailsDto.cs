namespace InnoGotchi.MVC.Models.Pet
{
    public class PetDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HungerLevel { get; set; }
        public string ThirstyLevel { get; set; }
        public int Age { get; set; }
        public bool IsAlive { get; set; }
        public double HappynessDayCount { get; set; }
        public int Body { get; set; }
        public int Eye { get; set; }
        public int Nose { get; set; }
        public int Mouth { get; set; }
    }
}
