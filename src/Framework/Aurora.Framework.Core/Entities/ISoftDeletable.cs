namespace Aurora.Framework;

public interface ISoftDeletable
{
    bool IsDeleted { get; }
    string? DeletedBy { get; }
    DateTime? DeletedAt { get; }
}