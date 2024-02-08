namespace Aurora.BizSuite.Settings.Domain.Options;

public record OptionId(int Value);

public record OptionItemId(int Value);

public enum OptionType
{
    System = 1,
    User = 2
}