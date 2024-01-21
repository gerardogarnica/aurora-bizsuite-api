namespace Aurora.Framework;

public abstract class AuditableEntity<TId> : BaseEntity<TId>
{
    protected AuditableEntity(TId id)
        : base(id) { }

    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}