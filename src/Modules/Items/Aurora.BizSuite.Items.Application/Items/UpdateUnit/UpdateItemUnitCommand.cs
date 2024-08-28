namespace Aurora.BizSuite.Items.Application.Items.UpdateUnit;

public sealed record UpdateItemUnitCommand(
    Guid ItemId,
    Guid ItemUnitId,
    bool AvailableForReceipt,
    bool AvailableForDispatch,
    bool UseDecimals)
    : ICommand;