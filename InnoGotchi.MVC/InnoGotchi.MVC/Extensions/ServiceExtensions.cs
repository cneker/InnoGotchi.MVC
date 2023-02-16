using FluentValidation;
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
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFarmService, FarmService>();
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
            var jwtSettings = configuration.GetSection("JwtSettings");
            var key = Environment.GetEnvironmentVariable("SECRET");

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

                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
        }

        public static void ConfigureCookies(this IServiceCollection services) =>
            services.ConfigureApplicationCookie(opts =>
                opts.ExpireTimeSpan = TimeSpan.FromDays(1));
    }
}
