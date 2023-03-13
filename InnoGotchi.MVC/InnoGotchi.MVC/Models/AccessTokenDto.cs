namespace InnoGotchi.MVC.Models
{
    public class AccessTokenDto
    {
        public Guid UserId { get; set; }
        public string AccessToken { get; set; }
    }
}
