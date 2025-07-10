using Application.Contracts;
using Identity.Models;
using Identity.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Identity.Services;

internal class TokenProvider(IOptions<JwtOptions> jwtOptions, UserManager<ApplicationUser> userManager) : ITokenProvider
{
    public async Task<string> GenerateAccessToken(Guid userId)
    {
        JwtOptions settings = jwtOptions.Value;

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(settings.SigningKey));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);        

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(await CollectUserClaimsAsync(userId)),
            Expires = DateTime.UtcNow.AddMinutes(settings.ExpirationInMinutes),
            SigningCredentials = signingCredentials,
            Issuer = settings.Issuer,
            Audience = settings.Audience
        };

        JsonWebTokenHandler handler = new();

        return handler.CreateToken(tokenDescriptor);
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }

    private async Task<IEnumerable<Claim>> CollectUserClaimsAsync(Guid userId)
    {
        ApplicationUser? user = await userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            throw new Exception("The requested user was not found.");
        }

        List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
        };

        IList<string> roles = await userManager.GetRolesAsync(user);

        claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

        return claims;
    }
}
