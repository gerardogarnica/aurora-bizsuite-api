namespace Aurora.BizSuite.Items.Application.Items.Update;

public sealed record UpdateItemCommand(
    Guid ItemId,
    string Name,
    string Description,
    Guid BrandId,
    string? AlternativeCode,
    string? Notes,
    List<string> Tags)
    : ICommand;