namespace Aurora.BizSuite.Items.Application.Items.UpdateDescription;

public sealed record UpdateItemDescriptionCommand(
    Guid ItemId,
    Guid ItemDescriptionId,
    string Description)
    : ICommand;