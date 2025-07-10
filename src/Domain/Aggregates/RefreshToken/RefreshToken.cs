using Domain.Common;

namespace Domain.Aggregates.RefreshToken;

public class RefreshToken : EntityBase<Guid>, IAggregateRoot
{
    public Guid UserId { get; private set; }
    public string Token { get; private set; } = string.Empty;
    public DateTime ExpiresOnUtc { get; private set; }

    private RefreshToken(Guid userId, string token, DateTime expiresOnUtc)
    {
        SetUserId(userId);
        SetToken(token);
        SetExpiration(expiresOnUtc);
    }

    public static RefreshToken Create(Guid userId, string token, DateTime expiresOnUtc) =>
        new(userId, token, expiresOnUtc);

    private void SetUserId(Guid userId)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException("User id must not be empty.", nameof(userId));
        }

        UserId = userId;
    }

    public void SetToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            throw new ArgumentException("Token must not be empty.", nameof(token));
        }

        Token = token;
    }

    public void SetExpiration(DateTime expiresOnUtc)
    {
        ExpiresOnUtc = expiresOnUtc;
    }

    public bool IsExpired() => DateTime.UtcNow >= ExpiresOnUtc;
}
