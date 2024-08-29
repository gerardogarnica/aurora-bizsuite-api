namespace Aurora.BizSuite.Items.Application.Items.AddRelated;

public sealed record AddRelatedItemCommand(
    Guid ItemId,
    Guid RelatedItemId)
    : ICommand;