using System.Text.RegularExpressions;

namespace Aurora.Framework;

public readonly record struct Email
{
    public string Value { get; }

    private Email(string value) => Value = value;

    public static Result<Email> Create(string email)
    {
        if (email == null)
            return Result.Fail<Email>(ValueObjectErrors.InvalidEmailAddress);

        var emailRegex = @"^[^@]+@[^@]+\.[a-zA-Z]{2,}$";
        if (!Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase))
            return Result.Fail<Email>(ValueObjectErrors.InvalidEmailAddress);

        return new Email(email);
    }
}