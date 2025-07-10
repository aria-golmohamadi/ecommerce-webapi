using Ardalis.Specification;

namespace Domain.Aggregates.RefreshToken.Specifications;

public class SingleRefreshTokenSpecification : SingleResultSpecification<RefreshToken>
{
    public SingleRefreshTokenSpecification(string token)
    {
        Query.Where(x => x.Token == token);
    }
}
