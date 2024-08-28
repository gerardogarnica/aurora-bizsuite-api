namespace Aurora.BizSuite.Items.Application.Items.AddDescription;

public sealed record AddItemDescriptionCommand(
    Guid ItemId,
    string Type,
    string Description)
    : ICommand;