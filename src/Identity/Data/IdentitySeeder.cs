using Identity.Models;
using Identity.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Identity.Data;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();

        RoleManager<ApplicationRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        InitialUserOptions userOptions = scope.ServiceProvider.GetRequiredService<IOptions<InitialUserOptions>>().Value;

        string[] roles = new[] { "User", "Admin" };

        foreach (string role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new ApplicationRole(role));
            }
        }

        ApplicationUser? user = await userManager.FindByEmailAsync(userOptions.Email);

        if (user == null)
        {
            user = new()
            {
                FirstName = userOptions.FirstName,
                LastName = userOptions.LastName,
                UserName = userOptions.UserName,
                Email = userOptions.Email,
                EmailConfirmed = true
            };

            IdentityResult result = await userManager.CreateAsync(user, userOptions.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
