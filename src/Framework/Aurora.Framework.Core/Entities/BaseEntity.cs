namespace Aurora.Framework;

public abstract class BaseEntity<TId>
{
    protected BaseEntity()
    {
        Id = default!;
    }

    protected BaseEntity(TId id)
    {
        Id = id;
    }

    public TId Id { get; init; }
}