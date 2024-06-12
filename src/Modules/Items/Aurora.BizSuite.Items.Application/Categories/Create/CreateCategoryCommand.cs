namespace Aurora.BizSuite.Items.Application.Categories.Create;

public sealed record CreateCategoryCommand(
    Guid? ParentId,
    string Name,
    string? Notes)
    : ICommand<Guid>;