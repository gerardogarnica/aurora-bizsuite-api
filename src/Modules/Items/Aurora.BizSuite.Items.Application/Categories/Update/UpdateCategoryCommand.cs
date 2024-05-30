namespace Aurora.BizSuite.Items.Application.Categories.Update;

public sealed record UpdateCategoryCommand(
    Guid CategoryId,
    string Name,
    string? Notes)
    : ICommand;