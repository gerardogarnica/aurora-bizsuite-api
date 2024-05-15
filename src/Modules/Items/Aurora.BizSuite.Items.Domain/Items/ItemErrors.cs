namespace Aurora.BizSuite.Items.Domain.Items;

public static class ItemErrors
{
    public static BaseError NotFound(Guid id) => new(
        "Items.NotFound",
        $"The item with identifier {id} was not found.");
}