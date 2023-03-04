using FluentValidation;
using InnoGotchi.Application.Exceptions;
using InnoGotchi.MVC.Clients;
using InnoGotchi.MVC.Contracts.Clients;
using InnoGotchi.MVC.Contracts.Helpers;
using InnoGotchi.MVC.Contracts.Services;
using InnoGotchi.MVC.Helpers;
using InnoGotchi.MVC.Mapper;
using InnoGotchi.MVC.Services;
using InnoGotchi.MVC.Validators.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace InnoGotchi.MVC.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            var webAPISettings = configuration.GetSection("WebAPIHttpClient");
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

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetRequiredSection("JwtSettings");
            var validIssuer = jwtSettings.GetRequiredSection("validIssuer").Value;
            var validAudience = jwtSettings.GetRequiredSection("validAudience").Value;
            var key = Environment.GetEnvironmentVariable("SECRET");
            if (key == null)
                throw new MissingEnvironmentVariableException("SECRET");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = validIssuer,
                    ValidAudience = validAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
        }

        public static void ConfigureCookies(this IServiceCollection services) =>
            services.ConfigureApplicationCookie(opts =>
                opts.ExpireTimeSpan = TimeSpan.FromDays(1));
    }
}
