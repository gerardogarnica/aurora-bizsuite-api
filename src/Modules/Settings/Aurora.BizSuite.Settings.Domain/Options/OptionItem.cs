namespace Aurora.BizSuite.Settings.Domain.Options;

public class OptionItem : BaseEntity<OptionItemId>
{
    public OptionId OptionId { get; private set; }
    public string Code { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    protected OptionItem() : base(new OptionItemId(0))
    {
        OptionId = new OptionId(0);
        Code = string.Empty;
        IsActive = true;
    }

    internal OptionItem(OptionId optionId, string code, string? description)
        : base(new OptionItemId(0))
    {
        OptionId = optionId;
        Code = code;
        Description = description?.Trim();
        IsActive = true;
    }

    internal void Update(string? description, bool isActive)
    {
        Description = description?.Trim();
        IsActive = isActive;
    }
}