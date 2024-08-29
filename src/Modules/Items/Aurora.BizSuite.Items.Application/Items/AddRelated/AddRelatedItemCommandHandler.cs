namespace Aurora.BizSuite.Items.Application.Items.AddRelated;

internal sealed class AddRelatedItemCommandHandler(
    IItemRepository itemRepository)
    : ICommandHandler<AddRelatedItemCommand>
{
    public async Task<Result> Handle(
        AddRelatedItemCommand request,
        CancellationToken cancellationToken)
    {
        // Get item
        var itemId = new ItemId(request.ItemId);
        var item = await itemRepository.GetByIdAsync(itemId);

        if (item is null)
        {
            return Result.Fail(ItemErrors.NotFound(request.ItemId));
        }

        // Get related item
        var relatedItemId = new ItemId(request.RelatedItemId);
        var relatedItem = await itemRepository.GetByIdAsync(relatedItemId);

        if (relatedItem is null)
        {
            return Result.Fail(ItemErrors.NotFound(request.RelatedItemId));
        }

        // Add related item
        var result = item.AddRelated(relatedItem);

        if (!result.IsSuccessful)
        {
            return Result.Fail(result.Error);
        }

        itemRepository.Update(result.Value);

        return Result.Ok();
    }
}