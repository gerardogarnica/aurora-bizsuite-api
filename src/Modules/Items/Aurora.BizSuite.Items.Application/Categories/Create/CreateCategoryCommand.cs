namespace Aurora.BizSuite.Items.Application.Categories.Create;

public sealed record CreateCategoryCommand(
    string Name,
    string? Notes)
    : ICommand<Guid>;