namespace Aurora.BizSuite.Items.Application.Items.AddUnit;

public sealed record AddItemUnitCommand(
    Guid ItemId,
    Guid UnitId,
    bool AvailableForReceipt,
    bool AvailableForDispatch,
    bool UseDecimals) 
    : ICommand;