namespace Aurora.BizSuite.Items.Application.Items.RemoveUnit;

public sealed record RemoveItemUnitCommand(Guid ItemId, Guid ItemUnitId) : ICommand;