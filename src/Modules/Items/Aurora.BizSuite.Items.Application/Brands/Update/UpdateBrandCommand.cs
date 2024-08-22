namespace Aurora.BizSuite.Items.Application.Brands.Update;

public sealed record UpdateBrandCommand(
    Guid BrandId,
    string Name,
    string? LogoUri,
    string? Notes)
    : ICommand;