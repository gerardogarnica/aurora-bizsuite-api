namespace Aurora.BizSuite.Security.Domain.Users;

public sealed class Password : ValueObject
{
    private const int MinLength = 8;

    public string Value { get; }

    private Password(string value) => Value = value;

    public static Result<Password> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Fail<Password>(DomainErrors.Password.PasswordNotNull);

        if (value.Length < MinLength)
            return Result.Fail<Password>(DomainErrors.Password.PasswordTooShort);

        return new Password(value);
    }

    public static implicit operator string(Password password) => password?.Value ?? string.Empty;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}