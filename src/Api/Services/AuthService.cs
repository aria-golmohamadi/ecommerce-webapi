using Api.Interfaces;
using Api.Requests.Auth;
using Application.Auth.Commands.LoginWithCookie;
using Application.Auth.Commands.LoginWithJwt;
using Application.Auth.Commands.Refresh;
using Application.Auth.Commands.Register;
using Application.Auth.DTOs;
using Ardalis.Result;
using MediatR;

namespace Api.Services;

public class AuthService(IMediator mediator) : IAuthService
{
    public async Task<Result<bool>> LoginWithCookieAsync(LoginRequest request)
    {
        string email = request.Email.Trim();
        string password = request.Password.Trim();

        return await mediator.Send(new LoginWithCookieCommand(email, password));
    }

    public async Task<Result<TokenDto>> LoginWithJwtAsync(LoginRequest request)
    {
        string email = request.Email.Trim();
        string password = request.Password.Trim();

        return await mediator.Send(new LoginWithJwtCommand(email, password));
    }

    public async Task<Result<TokenDto>> RefreshAsync(RefreshRequest request)
    {
        string refreshToken = request.RefreshToken.Trim();

        return await mediator.Send(new RefreshCommand(refreshToken));
    }

    public async Task<Result<bool>> RegisterAsync(RegisterRequest request)
    {
        string firstName = request.FirstName.Trim();
        string lastName = request.LastName.Trim();
        string userName = request.UserName.Trim();
        string email = request.Email.Trim();
        string password = request.Password.Trim();

        return await mediator.Send(new RegisterCommand(firstName, lastName, userName, email, password));
    }
}
