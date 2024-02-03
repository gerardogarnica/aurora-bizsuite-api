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