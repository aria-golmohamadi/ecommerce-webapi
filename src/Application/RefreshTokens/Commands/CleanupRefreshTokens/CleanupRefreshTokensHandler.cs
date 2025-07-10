using Application.Contracts;
using Ardalis.Result;
using Domain.Aggregates.RefreshToken;
using Domain.Aggregates.RefreshToken.Specifications;

namespace Application.RefreshTokens.Commands.CleanupRefreshTokens;

internal class CleanupRefreshTokensHandler(IRepository<RefreshToken> tokenRepository) : ICommandHandler<CleanupRefreshTokensCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(CleanupRefreshTokensCommand request, CancellationToken cancellationToken)
    {
        List<RefreshToken> tokens = await tokenRepository.ListAsync(new ExpiredRefreshTokensSpecification(), cancellationToken);

        foreach (RefreshToken token in tokens)
        {
            await tokenRepository.DeleteAsync(token, cancellationToken);
        }

        return Result<bool>.Success(true);
    }
}
