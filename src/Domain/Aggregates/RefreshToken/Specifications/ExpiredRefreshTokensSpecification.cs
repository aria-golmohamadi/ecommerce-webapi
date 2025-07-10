using Ardalis.Specification;

namespace Domain.Aggregates.RefreshToken.Specifications;

public class ExpiredRefreshTokensSpecification : Specification<RefreshToken>
{
    public ExpiredRefreshTokensSpecification()
    {
        Query.Where(x => x.ExpiresOnUtc < DateTime.UtcNow);
    }
}
