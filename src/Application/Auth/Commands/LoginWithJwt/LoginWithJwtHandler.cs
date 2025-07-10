using Application.Auth.DTOs;
using Application.Contracts;
using Ardalis.Result;
using Domain.Aggregates.RefreshToken;

namespace Application.Auth.Commands.LoginWithJwt;

internal class LoginWithJwtHandler(IUserService userService, ITokenProvider tokenProvider, IRepository<RefreshToken> tokenRepository) : ICommandHandler<LoginWithJwtCommand, Result<TokenDto>>
{
    public async Task<Result<TokenDto>> Handle(LoginWithJwtCommand request, CancellationToken cancellationToken)
    {
        Result<bool> credentialsCheckResult = await userService.CheckCredentialsAsync(request.Email, request.Password);

        if (credentialsCheckResult.Status == ResultStatus.Unauthorized)
        {
            return Result<TokenDto>.Unauthorized(credentialsCheckResult.Errors.ToArray());
        }

        Result<Guid> retrievalResult = await userService.RetrieveIdAsync(request.Email);

        if (retrievalResult.Status == ResultStatus.NotFound)
        {
            return Result<TokenDto>.NotFound(retrievalResult.Errors.ToArray());
        }

        Guid userId = (Guid)retrievalResult.GetValue();

        TokenDto response = new()
        {
            AccessToken = await tokenProvider.GenerateAccessToken(userId),
            RefreshToken = tokenProvider.GenerateRefreshToken()
        };

        await tokenRepository.AddAsync(RefreshToken.Create(userId, response.RefreshToken, DateTime.UtcNow.AddDays(7)), cancellationToken);

        return Result<TokenDto>.Success(response, "Access and refresh tokens have been generated.");
    }
}
