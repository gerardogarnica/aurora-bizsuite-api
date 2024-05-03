namespace Aurora.Framework;

public interface IAuditableEntity
{
    string? CreatedBy { get; }
    DateTime CreatedAt { get; }
    string? UpdatedBy { get; }
    DateTime? UpdatedAt { get; }
}