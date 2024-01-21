namespace Aurora.BizSuite.Settings.Domain.Options;

public class OptionItem : BaseEntity<OptionItemId>
{
    internal OptionItem(OptionId optionId, string code, string? description)
        : base(new OptionItemId(0))
    {
        OptionId = optionId;
        Code = code;
        Description = description?.Trim();
        IsActive = true;
    }

    public OptionId OptionId { get; private set; }
    public string Code { get; private set; }
    public string? Description { get; private set; }
    public bool IsActive { get; private set; }

    internal void Update(string? description, bool isActive)
    {
        Description = description?.Trim();
        IsActive = isActive;
    }
}