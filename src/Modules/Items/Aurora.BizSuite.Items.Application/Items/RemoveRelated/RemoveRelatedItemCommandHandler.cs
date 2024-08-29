namespace Aurora.BizSuite.Items.Application.Items.RemoveRelated;

internal sealed class RemoveRelatedItemCommandHandler(
    IItemRepository itemRepository)
    : ICommandHandler<RemoveRelatedItemCommand>
{
    public async Task<Result> Handle(
        RemoveRelatedItemCommand request,
        CancellationToken cancellationToken)
    {
        // Get item
        var itemId = new ItemId(request.ItemId);
        var item = await itemRepository.GetByIdAsync(itemId);

        if (item is null)
        {
            return Result.Fail(ItemErrors.NotFound(request.ItemId));
        }

        // Remove related item
        var result = item.RemoveRelated(request.RelatedItemId);

        if (!result.IsSuccessful)
        {
            return Result.Fail(result.Error);
        }

        itemRepository.Update(result.Value);

        return Result.Ok();
    }
}