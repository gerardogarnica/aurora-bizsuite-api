namespace Aurora.BizSuite.Settings.Domain.Options;

public class Option : AuditableEntity<OptionId>, IAggregateRoot
{
    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public OptionType Type { get; private set; }
    public IList<OptionItem> Items { get; private set; } = new List<OptionItem>();

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

    public static Option Create(string code, string name, string? description, OptionType type)
    {
        return new Option(code, name, description, type);
    }

    public Option Update(string name, string? description)
    {
        if (Type == OptionType.System)
            throw new Exception("System option cannot be updated.");

        Name = name.Trim();
        Description = description?.Trim();

        return this;
    }

    public void AddItem(string code, string? description)
    {
        if (Items.Any(x => x.Code == code))
            throw new Exception("Existing code.");
        //throw new OptionItemAlreadyExistsException(code, Code);
        Items.Add(new OptionItem(Id, code, description));
    }

    public void UpdateItem(string code, string? description, bool isActive)
    {
        var item = Items.FirstOrDefault(x => x.Code == code);
        item?.Update(description, isActive);
    }
}