using Microsoft.Extensions.Options;
using Quartz;

namespace Infrastructure.Jobs.CleanupRefreshTokens;

internal class CleanupRefreshTokensSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        JobKey jobKey = JobKey.Create(nameof(CleanupRefreshTokensJob));

        options
            .AddJob<CleanupRefreshTokensJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
            .AddTrigger(triggerBuilder => triggerBuilder.ForJob(jobKey).WithSimpleSchedule(scheduleBuilder => scheduleBuilder.WithIntervalInMinutes(60).RepeatForever()));
    }
}
