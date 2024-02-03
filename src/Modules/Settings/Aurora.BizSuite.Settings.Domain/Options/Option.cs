namespace Aurora.BizSuite.Settings.Domain.Options;

public class Option : AuditableEntity<OptionId>, IAggregateRoot
{
    private readonly List<OptionItem> _items = [];

    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public OptionType Type { get; private set; }
    public IReadOnlyList<OptionItem> Items => _items;

    protected Option() : base(new OptionId(0))
    {
        Code = string.Empty;
        Name = string.Empty;
        Type = OptionType.System;
    }

    private Option(string code, string name, string? description, OptionType type)
        : base(new OptionId(0))
    {
        Code = code;
        Name = name.Trim();
        Description = description?.Trim();
        Type = type;
    }

    public static Option Create(
        string code, string name, string? description, OptionType type)
    {
        return new Option(code, name, description, type);
    }

    public Result<Option> Update(string name, string? description)
    {
        if (Type == OptionType.System)
            return Result.Fail<Option>(DomainErrors.OptionErrors.SystemOptionNotAllowedToUpdate);

        Name = name.Trim();
        Description = description?.Trim();

        return this;
    }

    public Result AddItem(string code, string? description)
    {
        if (_items.Any(x => x.Code == code))
            return DomainErrors.OptionErrors.OptionItemCodeAlreadyExists;

        _items.Add(new OptionItem(Id, code, description));

        return Result.Ok();
    }

    public void UpdateItem(string code, string? description, bool isActive)
    {
        var item = _items.FirstOrDefault(x => x.Code == code);
        item?.Update(description, isActive);
    }
}