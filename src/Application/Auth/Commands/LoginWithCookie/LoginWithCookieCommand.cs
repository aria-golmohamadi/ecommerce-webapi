using Application.Contracts;
using Ardalis.Result;

namespace Application.Auth.Commands.LoginWithCookie;

public record LoginWithCookieCommand(string Email, string Password) : ICommand<Result<bool>>;
