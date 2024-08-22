namespace Aurora.BizSuite.Items.Application.Brands.GetById;

public sealed record GetBrandByIdQuery(Guid Id) : IQuery<BrandModel>;