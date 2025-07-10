using Application.Contracts;
using Identity.Options;
using Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity;

public static class ServiceRegistration
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureApplicationCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromDays(7);
        });

        services.Configure<SecurityStampValidatorOptions>(options =>
        {
            options.ValidationInterval = TimeSpan.FromMinutes(1);
        });

        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        services.Configure<InitialUserOptions>(configuration.GetSection("InitialUser"));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenProvider, TokenProvider>();

        return services;
    }
}
