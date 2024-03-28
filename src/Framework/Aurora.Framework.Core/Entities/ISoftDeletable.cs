namespace Aurora.Framework;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
    public string? DeletedBy { get; set; }
    DateTime? DeletedAt { get; set; }
}