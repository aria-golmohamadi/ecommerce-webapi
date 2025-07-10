using Api.Constants;
using Api.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Api.Extensions;

public static class AuthenticationExtensions
{
    public static AuthenticationBuilder AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        AuthenticationBuilder authenticationBuilder = services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme =
            options.DefaultChallengeScheme =
            options.DefaultForbidScheme =
            options.DefaultSignInScheme =
            options.DefaultSignOutScheme =
            options.DefaultScheme = AuthenticationSchemes.HybridScheme;
        });

        authenticationBuilder = authenticationBuilder.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["Jwt:Issuer"] ?? throw new JwtIssuerNotFoundException(),

                ValidateAudience = true,
                ValidAudience = configuration["Jwt:Audience"] ?? throw new JwtAudienceNotFoundException(),

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SigningKey"] ?? throw new JwtSigningKeyNotFoundException())),

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        authenticationBuilder = authenticationBuilder.AddPolicyScheme(AuthenticationSchemes.HybridScheme, "Hybrid Authentication", options =>
        {
            options.ForwardDefaultSelector = context =>
            {
                string useCookies = context.Request.Query["useCookies"].ToString();

                if (useCookies.Equals(true.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    return IdentityConstants.ApplicationScheme;
                }

                return JwtBearerDefaults.AuthenticationScheme;
            };
        });

        return authenticationBuilder;
    }
}
