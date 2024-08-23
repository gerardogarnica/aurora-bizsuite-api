namespace Aurora.BizSuite.Items.Application.Items.UpdateStatus;

internal sealed class UpdateItemStatusCommandHandler(
    IItemRepository itemRepository)
    : ICommandHandler<UpdateItemStatusCommand>
{
    public async Task<Result> Handle(
        UpdateItemStatusCommand request,
        CancellationToken cancellationToken)
    {
        // Get item
        var itemId = new ItemId(request.ItemId);
        var item = await itemRepository.GetByIdAsync(itemId);

        if (item is null)
        {
            return Result.Fail(ItemErrors.NotFound(request.ItemId));
        }

        // Update item status
        var result = request.Enable
            ? item.Enable()
            : item.Disable();

        if (!result.IsSuccessful)
        {
            return Result.Fail(result.Error);
        }

        itemRepository.Update(item);

        return Result.Ok();
    }
}