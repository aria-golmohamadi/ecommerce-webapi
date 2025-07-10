using Application.Auth.DTOs;
using Application.Contracts;
using Ardalis.Result;
using Domain.Aggregates.RefreshToken;
using Domain.Aggregates.RefreshToken.Specifications;

namespace Application.Auth.Commands.Refresh;

internal class RefreshHandler(IRepository<RefreshToken> tokenRepository, ITokenProvider tokenProvider) : ICommandHandler<RefreshCommand, Result<TokenDto>>
{
    public async Task<Result<TokenDto>> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        RefreshToken? refreshToken = await tokenRepository.FirstOrDefaultAsync(new SingleRefreshTokenSpecification(request.RefreshToken), cancellationToken);

        if (refreshToken == null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
        {
            return Result<TokenDto>.Invalid(new ValidationError(nameof(request.RefreshToken), "The provided refresh token is invalid or has expired."));
        }

        Guid userId = refreshToken.UserId;

        await tokenRepository.DeleteAsync(refreshToken, cancellationToken);

        TokenDto response = new()
        {
            AccessToken = await tokenProvider.GenerateAccessToken(userId),
            RefreshToken = tokenProvider.GenerateRefreshToken()
        };

        await tokenRepository.AddAsync(RefreshToken.Create(userId, response.RefreshToken, DateTime.UtcNow.AddDays(7)), cancellationToken);

        return Result<TokenDto>.Success(response, "Access and refresh tokens have been generated.");
    }
}
