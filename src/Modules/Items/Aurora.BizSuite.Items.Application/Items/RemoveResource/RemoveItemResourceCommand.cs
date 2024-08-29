namespace Aurora.BizSuite.Items.Application.Items.RemoveResource;

public sealed record RemoveItemResourceCommand(Guid ItemId, Guid ItemResourceId, string Type) : ICommand;