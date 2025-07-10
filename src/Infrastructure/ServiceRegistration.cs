using Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddQuartz();

        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        services.ConfigureJobs();

        return services;
    }
}
