namespace Aurora.BizSuite.Items.Application.Items.RemoveRelated;

public sealed record RemoveRelatedItemCommand(Guid ItemId, Guid RelatedItemId) : ICommand;