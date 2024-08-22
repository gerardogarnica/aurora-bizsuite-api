namespace Aurora.BizSuite.Items.Application.Items.Create;

public sealed record CreateItemCommand(
    string Code,
    string Name,
    string Description,
    Guid CategoryId,
    Guid BrandId,
    ItemType Type,
    string? AlternativeCode,
    string? Notes,
    List<string> Tags)
    : ICommand<Guid>;