namespace Aurora.BizSuite.Items.Application.Items.RemoveDescription;

public sealed record RemoveItemDescriptionCommand(Guid ItemId, Guid ItemDescriptionId) : ICommand;