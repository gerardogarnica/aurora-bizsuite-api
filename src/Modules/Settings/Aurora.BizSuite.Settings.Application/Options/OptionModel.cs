namespace Aurora.BizSuite.Settings.Application.Options;

public record OptionModel(
    int OptionId,
    string Code,
    string Name,
    string? Description,
    bool IsEditable,
    List<OptionItemModel> Items);

public record OptionItemModel(
    int ItemId,
    string Code,
    string? Description,
    bool IsActive);

internal static class OptionModelExtensions
{
    internal static OptionModel ToModel(this Option option)
    {
        return new OptionModel(
            option.Id.Value,
            option.Code,
            option.Name,
            option.Description,
            option.Type.Equals(OptionType.User),
            option.Items.Select(x => x.ToModel()).ToList());
    }

    internal static OptionItemModel ToModel(this OptionItem item)
    {
        return new OptionItemModel(
            item.Id.Value,
            item.Code,
            item.Description,
            item.IsActive);
    }
}