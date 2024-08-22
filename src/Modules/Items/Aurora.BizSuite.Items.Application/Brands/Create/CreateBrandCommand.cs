namespace Aurora.BizSuite.Items.Application.Brands.Create;

public sealed record CreateBrandCommand(
    string Name,
    string? LogoUri,
    string? Notes)
    : ICommand<Guid>;