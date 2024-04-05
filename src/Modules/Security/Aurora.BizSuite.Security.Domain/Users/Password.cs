namespace Aurora.BizSuite.Security.Domain.Users;

public sealed class Password : ValueObject
{
    private const int MinLength = 6;

    public string Value { get; }

    private Password(string value) => Value = value;

    public static Result<Password> Create(
        IPasswordProvider passwordProvider, 
        string unhashedValue)
    {
        if (string.IsNullOrWhiteSpace(unhashedValue))
            return Result.Fail<Password>(DomainErrors.Password.PasswordNotNull);

        if (unhashedValue.Length < MinLength)
            return Result.Fail<Password>(DomainErrors.Password.PasswordTooShort);

        var passwordHash = passwordProvider.HashPassword(unhashedValue);

        return new Password(passwordHash);
    }

    public static implicit operator string(Password password) => password?.Value ?? string.Empty;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}