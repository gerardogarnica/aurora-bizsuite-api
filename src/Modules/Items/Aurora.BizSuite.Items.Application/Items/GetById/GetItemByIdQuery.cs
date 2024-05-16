namespace Aurora.BizSuite.Items.Application.Items.GetById;

public sealed record GetItemByIdQuery(Guid Id) : IQuery<ItemModel>;