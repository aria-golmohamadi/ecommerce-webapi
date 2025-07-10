using Application.RefreshTokens.Commands.CleanupRefreshTokens;
using MediatR;
using Quartz;

namespace Infrastructure.Jobs.CleanupRefreshTokens;

[DisallowConcurrentExecution]
internal class CleanupRefreshTokensJob(IMediator mediator) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        await mediator.Send(new CleanupRefreshTokensCommand());
    }
}
