namespace Aurora.BizSuite.Items.Application.Items.SetPrimaryUnit;

public sealed record SetPrimaryItemUnitCommand(Guid ItemId, Guid ItemUnitId) : ICommand;