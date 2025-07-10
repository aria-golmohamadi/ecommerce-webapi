using Infrastructure.Jobs.CleanupRefreshTokens;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

internal static class JobsExtensions
{
    public static IServiceCollection ConfigureJobs(this IServiceCollection services)
    {
        services.ConfigureOptions<CleanupRefreshTokensSetup>();

        return services;
    }
}
