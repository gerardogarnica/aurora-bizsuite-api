using Aurora.BizSuite.Security.Domain.Roles;

namespace Aurora.BizSuite.Security.Domain.Users;

public class UserRole : AuditableEntity<UserRoleId>
{
    public UserId UserId { get; private set; }
    public RoleId RoleId { get; private set; }
    public bool IsEditable { get; private set; }
    //public User User { get; private set; } = null!;
    //public Role Role { get; private set; } = null!;

    protected UserRole()
    {
        UserId = new UserId(Guid.NewGuid());
        RoleId = new RoleId(Guid.NewGuid());
    }

    public UserRole(UserId userId, RoleId roleId, bool isEditable)
    {
        UserId = userId;
        RoleId = roleId;
        IsEditable = isEditable;
    }
}