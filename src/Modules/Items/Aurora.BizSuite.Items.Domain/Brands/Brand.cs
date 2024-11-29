using Aurora.BizSuite.Items.Domain.Items;

namespace Aurora.BizSuite.Items.Domain.Brands;

public sealed class Brand : AggregateRoot<BrandId>, IAuditableEntity, ISoftDeletable
{
    private readonly List<Item> _items = [];

    public string Name { get; private set; }
    public string? LogoUri { get; private set; }
    public string? Notes { get; private set; }
    public string? CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public bool IsDeleted { get; init; }
    public string? DeletedBy { get; init; }
    public DateTime? DeletedAt { get; init; }
    public IReadOnlyCollection<Item> Items => _items.AsReadOnly();

    private Brand() : base(new BrandId(Guid.NewGuid()))
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
        if (IsDeleted)
        {
            return Result.Fail<Brand>(BrandErrors.IsDeleted(Id.Value));
        }

        Name = name.Trim();
        LogoUri = logoUri?.Trim();
        Notes = notes?.Trim();

        return this;
    }

    public Result<Brand> Delete()
    {
        if (IsDeleted)
        {
            return Result.Fail<Brand>(BrandErrors.IsDeleted(Id.Value));
        }

        return this;
    }
}