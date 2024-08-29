namespace Aurora.BizSuite.Items.Application.Items.AddResource;

internal sealed class AddItemResourceCommandHandler(
    IItemRepository itemRepository)
    : ICommandHandler<AddItemResourceCommand>
{
    public async Task<Result> Handle(
        AddItemResourceCommand request,
        CancellationToken cancellationToken)
    {
        // Get item
        var itemId = new ItemId(request.ItemId);
        var item = await itemRepository.GetByIdAsync(itemId);

        if (item is null)
        {
            return Result.Fail(ItemErrors.NotFound(request.ItemId));
        }

        // Add item resource
        Result<Item>? result;

        if (request.Type == ItemConstants.ImageResourceTypeName)
        {
            result = item.AddImage(request.Uri);
        }
        else
        {
            result = item.AddDocument(request.Name!, request.Uri);
        }

        if (!result.IsSuccessful)
        {
            return Result.Fail(result.Error);
        }

        itemRepository.Update(result.Value);

        return Result.Ok();
    }
}