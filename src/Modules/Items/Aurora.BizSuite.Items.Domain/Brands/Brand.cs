namespace Aurora.BizSuite.Items.Domain.Brands;

public sealed class Brand : AggregateRoot<BrandId>, IAuditableEntity
{
    public string Name { get; private set; }
    public string? LogoUri { get; private set; }
    public string? Notes { get; private set; }
    public string? CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }

    private Brand() : base(new BrandId(Guid.Empty))
    {
        Name = string.Empty;
    }

    public static Brand Create(
        string name,
        string? logoUri,
        string? notes)
    {
        var brand = new Brand
        {
            Name = name.Trim(),
            LogoUri = logoUri,
            Notes = notes
        };

        return brand;
    }

    public Result<Brand> Update(
        string name,
        string? logoUri,
        string? notes)
    {
        Name = name.Trim();
        LogoUri = logoUri?.Trim();
        Notes = notes?.Trim();

        return this;
    }
}