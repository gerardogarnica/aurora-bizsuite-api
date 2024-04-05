using Aurora.BizSuite.Security.Domain.Roles;

namespace Aurora.BizSuite.Security.Domain.Users;

public class User : AggregateRoot<UserId>
{
    private string _passwordHash;
    private readonly List<UserRole> _roles = [];

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; private set; }
    public DateTime? PasswordExpirationDate { get; private set; }
    public string? Notes { get; private set; }
    public bool IsEditable { get; private set; }
    public UserStatusType Status { get; private set; }
    public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();

    protected User()
        : base(new UserId(Guid.NewGuid()))
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
        IsEditable = true;
        Status = UserStatusType.Pending;
        _passwordHash = string.Empty;
    }

    private User(
        string firstName, string lastName, string email,
        Password password, DateTime? expirationDate, string? notes,
        bool isEditable, UserStatusType statusType)
        : base(new UserId(Guid.NewGuid()))
    {
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim();
        PasswordExpirationDate = expirationDate;
        Notes = notes?.Trim();
        IsEditable = isEditable;
        Status = statusType;
        _passwordHash = password.Value;
    }

    public static User Create(
        string firstName, string lastName, string email, Password password,
        DateTime? expirationDate, string? notes, bool isEditable)
    {
        var user = new User(
            firstName,
            lastName,
            email,
            password,
            expirationDate,
            notes,
            isEditable,
            UserStatusType.Active);

        return user;
    }

    public static User Register(
        string firstName, string lastName, string email, Password password)
    {
        return new User(
            firstName,
            lastName,
            email,
            password,
            DateTime.Now.Date,
            null,
            true,
            UserStatusType.Pending);
    }

    public Result<User> Update(string firstName, string lastName, string? notes)
    {
        if (!IsEditable)
            return Result.Fail<User>(DomainErrors.UserErrors.UserIsNotEditable);

        if (Status is not UserStatusType.Active)
            return Result.Fail<User>(DomainErrors.UserErrors.UserIsNotActive);

        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Notes = notes?.Trim();

        return this;
    }

    public Result<User> Activate()
    {
        if (Status == UserStatusType.Active)
            return Result.Fail<User>(DomainErrors.UserErrors.UserAlreadyIsActive);

        Status = UserStatusType.Active;

        return this;
    }

    public Result<User> Inactivate()
    {
        if (Status is not UserStatusType.Active)
            return Result.Fail<User>(DomainErrors.UserErrors.UserIsNotActive);

        Status = UserStatusType.Inactive;

        return this;
    }

    public bool PasswordMatches(Password password)
    {
        if (string.IsNullOrWhiteSpace(password.Value)) return false;

        if (PasswordExpirationDate is not null && PasswordExpirationDate < DateTime.Now.Date) return false;

        return _passwordHash == password.Value;
    }

    public Result ChangePassword(Password oldPassword, Password newPassword)
    {
        if (_passwordHash != oldPassword.Value)
            return Result.Fail(DomainErrors.UserErrors.InvalidPassword);

        if (string.IsNullOrWhiteSpace(newPassword.Value))
            return Result.Fail(DomainErrors.UserErrors.InvalidPassword);

        _passwordHash = newPassword.Value;

        return Result.Ok();
    }

    public Result<User> AddRole(Role role, bool isEditable)
    {
        if (Status is not UserStatusType.Active)
            return Result.Fail<User>(DomainErrors.UserErrors.UserIsNotActive);

        if (role.IsDeleted)
            return Result.Fail<User>(DomainErrors.RoleErrors.RoleIsNotActive(role.Id.Value, role.Name));

        if (_roles.Any(x => x.RoleId == role.Id))
            return Result.Fail<User>(DomainErrors.UserErrors.RoleAlreadyAssigned);

        _roles.Add(new UserRole(Id, role.Id, isEditable));

        return this;
    }

    public Result<User> RemoveRole(Role role)
    {
        var userRole = _roles.FirstOrDefault(x => x.RoleId == role.Id);

        if (Status is not UserStatusType.Active)
            return Result.Fail<User>(DomainErrors.UserErrors.UserIsNotActive);

        if (role.IsDeleted)
            return Result.Fail<User>(DomainErrors.RoleErrors.RoleIsNotActive(role.Id.Value, role.Name));

        if (userRole is null)
            return Result.Fail<User>(DomainErrors.RoleErrors.RoleNotFound(role.Id.Value));

        if (!userRole.IsEditable)
            return Result.Fail<User>(DomainErrors.UserErrors.UserRoleIsUnableToRemove(role.Id.Value));

        _roles.Remove(userRole);

        return this;
    }
}