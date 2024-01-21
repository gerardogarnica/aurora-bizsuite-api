namespace Aurora.BizSuite.Settings.Domain.Options;

public class Option : AuditableEntity<OptionId>
{
    private Option(string code, string name, string? description, bool isVisible, bool isEditable)
        : base(new OptionId(0))
    {
        Code = code;
        Name = name.Trim();
        Description = description?.Trim();
        IsVisible = isVisible;
        IsEditable = isEditable;
    }

    public string Code { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public bool IsVisible { get; private set; }
    public bool IsEditable { get; private set; }
    public IList<OptionItem> Items { get; private set; } = new List<OptionItem>();

    public static Option Create(string code, string name, string? description, bool isVisible, bool isEditable)
    {
        return new Option(code, name, description, isVisible, isEditable);
    }

    public Option Update(string name, string? description, bool isVisible, bool isEditable)
    {
        Name = name.Trim();
        Description = description?.Trim();
        IsVisible = isVisible;
        IsEditable = isEditable;

        return this;
    }

    public void AddItem(string code, string? description)
    {
        Items.Add(new OptionItem(Id, code, description));
    }

    public void UpdateItem(string code, string? description, bool isActive)
    {
        var item = Items.FirstOrDefault(x => x.Code == code);
        item?.Update(description, isActive);
    }
}