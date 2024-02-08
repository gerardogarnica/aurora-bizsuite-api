namespace Aurora.Framework;

public interface IAuditableEntity
{
    public string? CreatedBy { get; }
    public DateTime CreatedAt { get; }
    public string? UpdatedBy { get; }
    public DateTime? UpdatedAt { get; }

    void SetCreated(string createdBy, DateTime createdAt);
    void SetUpdated(string updatedBy, DateTime updatedAt);
}

public abstract class AuditableEntity<TId> : BaseEntity<TId>, IAuditableEntity
{
    public string? CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string? UpdatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    protected AuditableEntity() => CreatedAt = DateTime.UtcNow;

    protected AuditableEntity(TId id)
        : base(id) => CreatedAt = DateTime.UtcNow;

    public void SetCreated(string createdBy, DateTime createdAt)
    {
        CreatedBy = createdBy;
        CreatedAt = createdAt;
    }

    public void SetUpdated(string updatedBy, DateTime updatedAt)
    {
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
    }
}