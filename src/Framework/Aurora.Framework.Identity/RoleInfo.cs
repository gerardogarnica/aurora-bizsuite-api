namespace Aurora.Framework.Identity;

public sealed class RoleInfo(
    Guid roleId,
    string name,
    string description,
    string? notes,
    bool isActive)
{
    public Guid RoleId { get; private set; } = roleId;
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
    public string? Notes { get; private set; } = notes;
    public bool IsActive { get; private set; } = isActive;
}