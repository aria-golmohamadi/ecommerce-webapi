using Application.Contracts;
using Ardalis.Result;

namespace Application.Auth.Commands.LoginWithCookie;

internal class LoginWithCookieHandler(IUserService userService) : ICommandHandler<LoginWithCookieCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(LoginWithCookieCommand request, CancellationToken cancellationToken)
    {
        Result<bool> credentialsCheckResult = await userService.CheckCredentialsAsync(request.Email, request.Password);

        if (credentialsCheckResult.Status == ResultStatus.Unauthorized)
        {
            return credentialsCheckResult;
        }

        return await userService.SignInAsync(request.Email);
    }
}
