using Api.Requests.Auth;
using Application.Auth.DTOs;
using Ardalis.Result;

namespace Api.Interfaces;

public interface IAuthService
{
    Task<Result<bool>> LoginWithCookieAsync(LoginRequest request);
    Task<Result<TokenDto>> LoginWithJwtAsync(LoginRequest request);
    Task<Result<TokenDto>> RefreshAsync(RefreshRequest request);
    Task<Result<bool>> RegisterAsync(RegisterRequest request);
}
