using Application.Auth.DTOs;
using Application.Contracts;
using Ardalis.Result;

namespace Application.Auth.Commands.Refresh;

public record RefreshCommand(string RefreshToken) : ICommand<Result<TokenDto>>;
