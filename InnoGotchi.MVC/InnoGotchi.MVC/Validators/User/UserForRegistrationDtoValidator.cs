using FluentValidation;
using InnoGotchi.MVC.Models.User;

namespace InnoGotchi.MVC.Validators.User
{
    public class UserForRegistrationDtoValidator : AbstractValidator<UserForRegistrationDto>
    {
        public UserForRegistrationDtoValidator()
        {
            RuleFor(u => u.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(u => u.FirstName)
                .NotEmpty()
                .Length(1, 30);
            RuleFor(u => u.LastName)
                .NotEmpty()
                .Length(1, 30);
            RuleFor(u => u.ConfirmedPassword)
                .NotEmpty()
                .Equal(u => u.Password);
        }
    }
}
