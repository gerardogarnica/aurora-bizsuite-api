using Aurora.BizSuite.Items.Domain.Brands;
using Aurora.BizSuite.Items.Domain.Categories;
using Aurora.BizSuite.Items.Domain.Units;

namespace Aurora.BizSuite.Items.Domain.Items;

public sealed class Item : AggregateRoot<ItemId>, IAuditableEntity
{
    const int maxNumberOfUnits = 9;

    private readonly List<ItemUnit> _units = [];

    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public BrandId BrandId { get; private set; }
    public ItemType ItemType { get; private set; }
    public string? AlternativeCode { get; private set; }
    public string? Notes { get; private set; }
    public string? Tags { get; private set; }
    public ItemStatus Status { get; private set; }
    public string? CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public Category Category { get; init; } = null!;
    public Brand Brand { get; init; } = null!;
    public IReadOnlyCollection<ItemUnit> Units => _units.AsReadOnly();

    private Item() : base(new ItemId(Guid.NewGuid()))
    {
        Code = string.Empty;
        Name = string.Empty;
        Description = string.Empty;
        CategoryId = null!;
        BrandId = null!;
    }

    public static Item Create(
        string code,
        string name,
        string description,
        Category category,
        Brand brand,
        ItemType itemType,
        string? alternativeCode,
        string? notes,
        List<string> tags)
    {
        var item = new Item
        {
            Code = code.Trim(),
            Name = name.Trim(),
            Description = description.Trim(),
            CategoryId = category.Id,
            BrandId = brand.Id,
            ItemType = itemType,
            AlternativeCode = alternativeCode?.Trim(),
            Notes = notes?.Trim(),
            Status = ItemStatus.Draft,
            Tags = string.Join(";", tags)
        };

        item.AddDomainEvent(new ItemCreatedDomainEvent(item.Id.Value));

        return item;
    }

    public Result<Item> Update(
        string name,
        string description,
        Brand brand,
        string? alternativeCode,
        string? notes,
        List<string> tags)
    {
        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        Name = name.Trim();
        Description = description.Trim();
        BrandId = brand.Id;
        AlternativeCode = alternativeCode?.Trim();
        Notes = notes?.Trim();
        Tags = string.Join(";", tags);

        AddDomainEvent(new ItemUpdatedDomainEvent(Id.Value));

        return this;
    }

    public Result<Item> Enable()
    {
        if (Status is ItemStatus.Active)
            return Result.Fail<Item>(ItemErrors.ItemAlreadyIsActive);

        if (_units.Count == 0)
            return Result.Fail<Item>(ItemErrors.ItemWithoutUnits);

        Status = ItemStatus.Active;

        AddDomainEvent(new ItemActivatedDomainEvent(Id.Value));

        return this;
    }

    public Result<Item> Disable()
    {
        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemAlreadyIsDisabled);

        Status = ItemStatus.Disabled;

        AddDomainEvent(new ItemDisabledDomainEvent(Id.Value));

        return this;
    }

    public Result<Item> AddUnit(
        UnitOfMeasurement unit,
        bool availableForReceipt,
        bool availableForDispatch,
        bool useDecimals)
    {
        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        if (_units.Any(x => x.UnitId == unit.Id))
            return Result.Fail<Item>(ItemErrors.ItemUnitAlreadyExists);

        if (_units.Count >= maxNumberOfUnits)
            return Result.Fail<Item>(ItemErrors.MaxNumberOfUnitsReached);

        var isPrimary = _units.Count == 0;

        var itemUnit = ItemUnit.Create(
            Id,
            unit.Id,
            isPrimary,
            availableForReceipt,
            availableForDispatch,
            useDecimals);

        _units.Add(itemUnit);

        return this;
    }

    public Result<Item> UpdateUnit(
        UnitOfMeasurement unit,
        bool availableForReceipt,
        bool availableForDispatch,
        bool useDecimals)
    {
        var itemUnit = _units.FirstOrDefault(x => x.UnitId == unit.Id);

        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        if (itemUnit is null)
            return Result.Fail<Item>(UnitErrors.NotFound(unit.Id.Value));

        itemUnit.Update(
            itemUnit.IsPrimary,
            availableForReceipt,
            availableForDispatch,
            useDecimals);

        return this;
    }

    public Result<Item> RemoveUnit(UnitOfMeasurement unit)
    {
        var itemUnit = _units.FirstOrDefault(x => x.UnitId == unit.Id);

        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        if (itemUnit is null)
            return Result.Fail<Item>(UnitErrors.NotFound(unit.Id.Value));

        if (itemUnit.IsPrimary)
            return Result.Fail<Item>(ItemErrors.ItemUnitIsUnableToRemove);

        _units.Remove(itemUnit);

        return this;
    }

    public Result<Item> SetPrimaryUnit(UnitOfMeasurement unit)
    {
        var itemUnit = _units.FirstOrDefault(x => x.UnitId == unit.Id);

        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        if (itemUnit is null)
            return Result.Fail<Item>(UnitErrors.NotFound(unit.Id.Value));

        if (itemUnit.IsPrimary)
            return Result.Fail<Item>(ItemErrors.ItemUnitIsPrimary);

        RemovePrimaryUnit();

        itemUnit.Update(
            true,
            itemUnit.AvailableForReceipt,
            itemUnit.AvailableForDispatch,
            itemUnit.UseDecimals);

        return this;
    }

    private void RemovePrimaryUnit()
    {
        var itemUnit = _units.FirstOrDefault(x => x.IsPrimary);

        if (itemUnit is null) return;

        itemUnit.Update(
            false,
            itemUnit.AvailableForReceipt,
            itemUnit.AvailableForDispatch,
            itemUnit.UseDecimals);
    }
}