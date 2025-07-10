using Application.Auth.DTOs;
using Application.Contracts;
using Ardalis.Result;

namespace Application.Auth.Commands.LoginWithJwt;

public record LoginWithJwtCommand(string Email, string Password) : ICommand<Result<TokenDto>>;