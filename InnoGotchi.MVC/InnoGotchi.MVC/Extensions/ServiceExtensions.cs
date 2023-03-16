using FluentValidation;
using InnoGotchi.MVC.Clients;
using InnoGotchi.MVC.Contracts.Clients;
using InnoGotchi.MVC.Contracts.Helpers;
using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.Helpers;
using InnoGotchi.MVC.Mapper;
using InnoGotchi.MVC.Services;
using InnoGotchi.MVC.Validators.User;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace InnoGotchi.MVC.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            var webAPISettings = configuration.GetRequiredSection("WebAPIHttpClient");
            var name = webAPISettings.GetRequiredSection("Name").Value;
            var url = webAPISettings.GetRequiredSection("BaseUrl").Value;
            services.AddHttpClient(name, conf =>
            {
                conf.BaseAddress = new Uri(url);
            });
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFarmService, FarmService>();
            services.AddScoped<IPetService, PetService>();
        }

        public static void ConfigureClients(this IServiceCollection services)
        {
            services.AddScoped<IUserClient, UserClient>();
            services.AddScoped<IFarmClient, FarmClient>();
            services.AddScoped<IAuthenticationClient, AuthenticationClient>();
            services.AddScoped<IPetClient, PetClient>();
        }

        public static void ConfigureHelpers(this IServiceCollection services)
        {
            services.AddScoped<IConvertHelper, ConvertHelper>();
        }

        public static void CongifureMapping(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(MappingProfile));

        public static void ConfigureValidators(this IServiceCollection services) =>
            services.AddValidatorsFromAssemblyContaining<UserForRegistrationDtoValidator>();

        public static void ConfigureCookies(this IServiceCollection services) =>
            services.ConfigureApplicationCookie(opts =>
                opts.ExpireTimeSpan = TimeSpan.FromDays(1));

        public static void ConfigureCookieAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt => opt.LoginPath = "/Authentication/SignIn");
        }
    }
}
