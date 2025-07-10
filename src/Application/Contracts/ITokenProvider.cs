namespace Application.Contracts;

public interface ITokenProvider
{
    Task<string> GenerateAccessToken(Guid userId);
    string GenerateRefreshToken();
}
