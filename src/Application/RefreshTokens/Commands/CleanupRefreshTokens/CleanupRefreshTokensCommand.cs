using Application.Contracts;
using Ardalis.Result;

namespace Application.RefreshTokens.Commands.CleanupRefreshTokens;

public record CleanupRefreshTokensCommand : ICommand<Result<bool>>;
