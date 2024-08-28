namespace Aurora.BizSuite.Items.Application.Items.SetPrimaryUnit;

internal sealed class SetPrimaryItemUnitCommandHandler(
    IItemRepository itemRepository)
    : ICommandHandler<SetPrimaryItemUnitCommand>
{
    public async Task<Result> Handle(
        SetPrimaryItemUnitCommand request,
        CancellationToken cancellationToken)
    {
        // Get item
        var itemId = new ItemId(request.ItemId);
        var item = await itemRepository.GetByIdAsync(itemId);

        if (item is null)
        {
            return Result.Fail(ItemErrors.NotFound(request.ItemId));
        }

        // Set primary item unit
        var result = item.SetPrimaryUnit(request.ItemUnitId);

        if (!result.IsSuccessful)
        {
            return Result.Fail(result.Error);
        }

        itemRepository.Update(result.Value);

        return Result.Ok();
    }
}