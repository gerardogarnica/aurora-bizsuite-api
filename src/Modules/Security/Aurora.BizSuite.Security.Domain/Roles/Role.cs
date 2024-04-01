using Aurora.BizSuite.Security.Domain.Users;
using ApplicationId = Aurora.BizSuite.Security.Domain.Applications.ApplicationId;

namespace Aurora.BizSuite.Security.Domain.Roles;

public class Role : AggregateRoot<RoleId>
{
    private readonly List<UserRole> _users = [];

    public ApplicationId ApplicationId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string? Notes { get; private set; }
    public bool IsActive { get; private set; }
    public IReadOnlyCollection<UserRole> Users => _users.AsReadOnly();

    protected Role()
        : base(new RoleId(Guid.NewGuid()))
    {
        ApplicationId = new ApplicationId(Guid.NewGuid());
        Name = string.Empty;
        Description = string.Empty;
        IsActive = true;
    }

    private Role(
        ApplicationId applicationId, string name, string description, string? notes, bool isActive)
        : base(new RoleId(Guid.NewGuid()))
    {
        ApplicationId = applicationId;
        Name = name.Trim();
        Description = description.Trim();
        Notes = notes?.Trim();
        IsActive = isActive;
    }

    public static Role Create(
        ApplicationId applicationId, string name, string description, string? notes)
    {
        return new Role(
            applicationId,
            name,
            description,
            notes,
            true);
    }

    public Result<Role> Update(
        string name, string description, string? notes)
    {
        if (!IsActive)
            return Result.Fail<Role>(DomainErrors.RoleErrors.RoleIsNotActive(Id.Value, name));

        Name = name.Trim();
        Description = description.Trim();
        Notes = notes?.Trim();

        return this;
    }

    public Result<Role> Activate()
    {
        if (IsActive)
            return Result.Fail<Role>(DomainErrors.RoleErrors.RoleAlreadyIsActive);

        IsActive = true;

        return this;
    }

    public Result<Role> Inactivate()
    {
        if (!IsActive)
            return Result.Fail<Role>(DomainErrors.RoleErrors.RoleIsNotActive(Id.Value, Name));

        IsActive = false;

        return this;
    }
}