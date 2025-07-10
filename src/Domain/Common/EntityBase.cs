namespace Domain.Common;

public abstract class EntityBase : EntityBase<Guid>
{
}

public abstract class EntityBase<TKey> : IAuditableEntity
{
    public virtual TKey Id { get; set; } = default!;
    public virtual DateTime CreatedOnUtc { get; set; }
    public virtual DateTime UpdatedOnUtc { get; set; }
}
