using Api.Interfaces;
using Api.Requests.Auth;
using Application.Auth.DTOs;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [TranslateResultToActionResult]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<Result<bool>> Register([FromBody] RegisterRequest request)
        {
            return await authService.RegisterAsync(request);
        }

        [HttpPost("login")]
        public async Task<Ardalis.Result.IResult> Login([FromBody] LoginRequest request, [FromQuery] bool useCookies = false)
        {
            if (useCookies)
            {
                return await authService.LoginWithCookieAsync(request);
            }
            else
            {
                return await authService.LoginWithJwtAsync(request);
            }
        }

        [HttpPost("refresh")]
        public async Task<Result<TokenDto>> Refresh([FromBody] RefreshRequest request)
        {
            return await authService.RefreshAsync(request);
        }
    }
}
