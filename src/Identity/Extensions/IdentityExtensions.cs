using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Extensions;

public static class IdentityExtensions
{
    private static readonly Action<IdentityOptions> setupAction = options =>
    {
        options.User.RequireUniqueEmail = true;
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";

        options.Password.RequireDigit =
        options.Password.RequireLowercase =
        options.Password.RequireUppercase =
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 8;

        options.SignIn.RequireConfirmedEmail = true;
    };

    public static IdentityBuilder AddIdentity(this IServiceCollection services) =>
        services.AddIdentity<ApplicationUser, ApplicationRole>(setupAction)
        .AddDefaultTokenProviders();
}
