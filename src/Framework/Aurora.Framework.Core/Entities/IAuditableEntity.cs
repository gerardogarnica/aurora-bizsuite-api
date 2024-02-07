namespace Aurora.Framework;

public interface IAuditableEntity
{
    public string? CreatedBy { get; }
    public DateTime CreatedAt { get; }
    public string? UpdatedBy { get; }
    public DateTime? UpdatedAt { get; }
}