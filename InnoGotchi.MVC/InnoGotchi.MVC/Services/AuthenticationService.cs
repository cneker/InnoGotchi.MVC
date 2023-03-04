using AutoMapper;
using InnoGotchi.MVC.Contracts.Clients;
using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.Models;
using InnoGotchi.MVC.Models.User;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace InnoGotchi.MVC.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IUserClient _userClient;
        private readonly IAuthenticationClient _authenticationClient;

        public AuthenticationService(IMapper mapper, IUserClient userClient, IAuthenticationClient authenticationClient)
        {
            _mapper = mapper;
            _userClient = userClient;
            _authenticationClient = authenticationClient;
        }

        public async Task<UserForAuthenticationDto> RegisterUserAsync(UserForRegistrationDto userForReg, ModelStateDictionary modelState)
        {
            var user = await _userClient.CreateUserAsync(userForReg);
            if (user == null)
            {
                modelState.AddModelError("Error", "This email has already registered");
                return null;
            }
            var userForAuth = _mapper.Map<UserForAuthenticationDto>(user);
            return userForAuth;
        }

        public async Task<AccessTokenDto> SignInAsync(UserForAuthenticationDto userForAuth, ModelStateDictionary modelState)
        {
            var token = await _authenticationClient.SignInAsync(userForAuth);
            if (token == null)
            {
                modelState.AddModelError("Error", "User not found! May be your email or password is wrong");
                return null;
            }
            return token;
        }
    }
}
