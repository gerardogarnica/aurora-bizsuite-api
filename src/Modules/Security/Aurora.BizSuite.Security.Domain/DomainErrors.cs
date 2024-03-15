namespace Aurora.BizSuite.Security.Domain;

public static class DomainErrors
{
    public static class Password
    {
        public static BaseError PasswordNotNull => new(
            "Password.NotNull",
            "The password is required.");

        public static BaseError PasswordTooShort => new(
            "Password.TooShort",
            "The password must be at least 8 characters long.");
    }

    public static class RoleErrors
    {
        public static readonly BaseError RoleNotFound = new(
            "Roles.NotFound",
            "Role not found.");

        public static readonly BaseError RoleAlreadyExists = new(
            "Roles.AlreadyExists",
            "Role already exists and cannot be created again.");

        public static readonly BaseError RoleAlreadyIsActive = new(
            "Roles.AlreadyIsActive",
            "Role already is active and cannot be activate again.");

        public static readonly BaseError RoleIsNotActive = new(
            "Roles.RoleIsNotActive",
            "Role is not active.");
    }

    public static class UserErrors
    {
        public static readonly BaseError InvalidCredentials = new(
            "Users.InvalidCredentials",
            "The email or password are incorrect.");

        public static readonly BaseError InvalidPassword = new(
            "Users.InvalidPassword",
            "The password is incorrect.");

        public static readonly BaseError RoleAlreadyAssigned = new(
            "Users.RoleAlreadyAssigned",
            "Role already is assigned to the user.");

        public static readonly BaseError UserNotFound = new(
            "Users.NotFound",
            "User not found.");

        public static readonly BaseError UserAlreadyExists = new(
            "Users.AlreadyExists",
            "User already exists and cannot be created again.");

        public static readonly BaseError UserAlreadyIsActive = new(
            "Users.AlreadyIsActive",
            "User already is active and cannot be activate again.");

        public static readonly BaseError UserIsNotEditable = new(
            "Users.UserIsNotEditable",
            "User is not editable.");

        public static readonly BaseError UserIsNotActive = new(
            "Users.UserIsNotActive",
            "User is not active.");

        public static readonly BaseError UserIsNotPending = new(
            "Users.UserIsNotPending",
            "User is not pending to activate.");

        public static readonly BaseError UserIsNotInactive = new(
            "Users.UserIsNotInactive",
            "User is not inactive.");
    }
}