namespace Aurora.BizSuite.Items.Application.Brands.Delete;

public sealed record DeleteBrandCommand(Guid BrandId) : ICommand;