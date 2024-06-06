using Aurora.BizSuite.Items.Domain.Categories;
using Aurora.BizSuite.Items.Domain.Units;

namespace Aurora.BizSuite.Items.Domain.Items;

public sealed class Item : AggregateRoot<ItemId>, IAuditableEntity
{
    private readonly List<ItemUnit> _units = [];

    public string Code { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public ItemType ItemType { get; private set; }
    public UnitOfMeasurementId MainUnitId { get; private set; }
    public string? AlternativeCode { get; private set; }
    public string? Notes { get; private set; }
    public ItemStatus Status { get; private set; }
    public string? CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public Category Category { get; init; } = null!;
    public UnitOfMeasurement MainUnit { get; init; } = null!;
    public IReadOnlyCollection<ItemUnit> Units => _units.AsReadOnly();

    private Item() : base(new ItemId(Guid.NewGuid()))
    {
        Code = string.Empty;
        Name = string.Empty;
        Description = string.Empty;
        CategoryId = null!;
        MainUnitId = null!;
    }

    public static Item Create(
        string code,
        string name,
        string description,
        Category category,
        ItemType itemType,
        UnitOfMeasurement mainUnit,
        string? alternativeCode,
        string? notes)
    {
        var item = new Item
        {
            Code = code.Trim(),
            Name = name.Trim(),
            Description = description.Trim(),
            CategoryId = category.Id,
            ItemType = itemType,
            MainUnitId = mainUnit.Id,
            AlternativeCode = alternativeCode?.Trim(),
            Notes = notes?.Trim(),
            Status = ItemStatus.Draft,
        };

        var itemUnit = new ItemUnit(
            item.Id,
            mainUnit.Id,
            1.00m,
            true,
            true,
            false);

        item._units.Add(itemUnit);

        item.AddDomainEvent(new ItemCreatedDomainEvent(item.Id.Value));

        return item;
    }

    public Result<Item> Update(
        string name,
        string description,
        UnitOfMeasurement mainUnit,
        string? alternativeCode,
        string? notes)
    {
        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        if (Status is ItemStatus.Active)
        {
            if (mainUnit.Id != MainUnitId)
                return Result.Fail<Item>(ItemErrors.MainUnitIsUnableToUpdate);
        }

        Name = name.Trim();
        Description = description.Trim();
        MainUnitId = mainUnit.Id;
        AlternativeCode = alternativeCode?.Trim();
        Notes = notes?.Trim();

        AddDomainEvent(new ItemUpdatedDomainEvent(Id.Value));

        return this;
    }

    public Result<Item> Activate()
    {
        if (Status is ItemStatus.Active)
            return Result.Fail<Item>(ItemErrors.ItemAlreadyIsActive);

        Status = ItemStatus.Active;

        AddDomainEvent(new ItemActivatedDomainEvent(Id.Value));

        return this;
    }

    public Result<Item> Disable(string reason)
    {
        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemAlreadyIsDisabled);

        Status = ItemStatus.Disabled;

        AddDomainEvent(new ItemDisabledDomainEvent(Id.Value));

        return this;
    }

    public Result<Item> AddUnit(
        UnitOfMeasurement unit,
        decimal conversionValue,
        bool availableForReceipt,
        bool availableForDispatch,
        bool useDecimals)
    {
        if (conversionValue <= decimal.Zero)
            return Result.Fail<Item>(ItemErrors.ItemUnitInvalidConversionValue);

        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        if (_units.Any(x => x.UnitId == unit.Id))
            return Result.Fail<Item>(ItemErrors.ItemUnitAlreadyExists);

        _units.Add(new ItemUnit(
            Id,
            unit.Id,
            conversionValue,
            availableForReceipt,
            availableForDispatch,
            useDecimals));

        return this;
    }

    public Result<Item> UpdateUnit(
        UnitOfMeasurement unit,
        decimal conversionValue,
        bool availableForReceipt,
        bool availableForDispatch,
        bool useDecimals)
    {
        var itemUnit = _units.FirstOrDefault(x => x.UnitId == unit.Id);

        if (conversionValue <= decimal.Zero)
            return Result.Fail<Item>(ItemErrors.ItemUnitInvalidConversionValue);

        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        if (itemUnit is null)
            return Result.Fail<Item>(UnitErrors.NotFound(unit.Id.Value));

        if (itemUnit.UnitId == MainUnitId)
            return Result.Fail<Item>(ItemErrors.ItemUnitIsUnableToUpdate);

        itemUnit.Update(
            conversionValue,
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

        if (itemUnit.UnitId == MainUnitId)
            return Result.Fail<Item>(ItemErrors.ItemUnitIsUnableToRemove);

        _units.Remove(itemUnit);

        return this;
    }
}