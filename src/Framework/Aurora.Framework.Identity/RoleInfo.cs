namespace Aurora.Framework.Identity;

public sealed class RoleInfo
{
    public Guid RoleId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string? Notes { get; private set; }
    public bool IsActive { get; private set; }
}