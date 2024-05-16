namespace Aurora.BizSuite.Items.Application.Categories.GetById;

public sealed record GetCategoryByIdQuery(Guid Id) : IQuery<CategoryModel>;