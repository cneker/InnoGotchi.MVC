namespace InnoGotchi.MVC.Models.User
{
    public class UserForRegistrationDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
