namespace Aurora.BizSuite.Items.Application.Items.AddResource;

public sealed record AddItemResourceCommand(
    Guid ItemId,
    string Uri,
    string Type,
    string? Name)
    : ICommand;