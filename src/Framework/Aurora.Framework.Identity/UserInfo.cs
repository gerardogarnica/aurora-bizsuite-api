namespace Aurora.Framework.Identity;

public sealed class UserInfo(
    Guid userId,
    string email,
    string firstName,
    string lastName,
    DateTime? passwordExpirationDate,
    string? notes,
    bool isEditable,
    IList<RoleInfo> roles)
{
    private readonly List<RoleInfo> _roles = [.. roles];

    public Guid UserId { get; private set; } = userId;
    public string Email { get; private set; } = email;
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;
    public string FullName => $"{FirstName} {LastName}";
    public bool PasswordMustChange
    {
        get
        {
            return PasswordExpirationDate.HasValue && PasswordExpirationDate.Value.Date <= DateTime.UtcNow.Date;
        }
    }
    public DateTime? PasswordExpirationDate { get; private set; } = passwordExpirationDate;
    public string? Notes { get; private set; } = notes;
    public bool IsEditable { get; private set; } = isEditable;
    public IReadOnlyList<RoleInfo> Roles => _roles;
}