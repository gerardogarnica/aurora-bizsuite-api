﻿using Aurora.BizSuite.Items.Domain.Brands;
using Aurora.BizSuite.Items.Domain.Categories;
using Aurora.BizSuite.Items.Domain.Units;

namespace Aurora.BizSuite.Items.Domain.Items;

public sealed class Item : AggregateRoot<ItemId>, IAuditableEntity
{
    const int maxNumberOfUnits = 9;
    const string imageResourceTypeName = "Image";

    private readonly List<ItemDescription> _descriptions = [];
    private readonly List<ItemResource> _resources = [];
    private readonly List<ItemUnit> _units = [];
    private readonly List<RelatedItem> _relatedItems = [];

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
    public IReadOnlyCollection<ItemDescription> Descriptions => _descriptions.AsReadOnly();
    public IReadOnlyCollection<ItemResource> Resources => _resources.AsReadOnly();
    public IReadOnlyCollection<ItemUnit> Units => _units.AsReadOnly();
    public IReadOnlyCollection<RelatedItem> RelatedItems => _relatedItems.AsReadOnly();

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

        foreach (var itemUnit in _units)
            itemUnit.SetIsEditable(false);

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
        Guid itemUnitId,
        bool availableForReceipt,
        bool availableForDispatch,
        bool useDecimals)
    {
        var itemUnit = _units.FirstOrDefault(x => x.Id == itemUnitId);

        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        if (itemUnit is null)
            return Result.Fail<Item>(UnitErrors.NotFound(itemUnitId));

        if (!itemUnit.IsEditable)
            return Result.Fail<Item>(ItemErrors.ItemUnitIsNotEditable);

        itemUnit.Update(
            itemUnit.IsPrimary,
            availableForReceipt,
            availableForDispatch,
            useDecimals);

        return this;
    }

    public Result<Item> RemoveUnit(Guid itemUnitId)
    {
        var itemUnit = _units.FirstOrDefault(x => x.Id == itemUnitId);

        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        if (itemUnit is null)
            return Result.Fail<Item>(UnitErrors.NotFound(itemUnitId));

        if (!itemUnit.IsEditable)
            return Result.Fail<Item>(ItemErrors.ItemUnitIsNotEditable);

        if (Status is not ItemStatus.Draft && itemUnit.IsPrimary)
            return Result.Fail<Item>(ItemErrors.ItemUnitIsUnableToRemove);

        _units.Remove(itemUnit);

        return this;
    }

    public Result<Item> SetPrimaryUnit(Guid itemUnitId)
    {
        var itemUnit = _units.FirstOrDefault(x => x.Id == itemUnitId);

        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        if (itemUnit is null)
            return Result.Fail<Item>(UnitErrors.NotFound(itemUnitId));

        if (!itemUnit.IsEditable)
            return Result.Fail<Item>(ItemErrors.ItemUnitIsNotEditable);

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

    public Result<Item> AddDescription(string type, string description)
    {
        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        if (_descriptions.Any(x => x.Type == type))
        {
            return Result.Fail<Item>(ItemErrors.ItemDescriptionAlreadyExists);
        }

        var itemDescription = ItemDescription.Create(
            Id,
            type,
            description);

        _descriptions.Add(itemDescription);

        return this;
    }

    public Result<Item> UpdateDescription(Guid descriptionId, string description)
    {
        var itemDescription = _descriptions.FirstOrDefault(x => x.Id == descriptionId);

        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        if (itemDescription is null)
            return Result.Fail<Item>(ItemErrors.ItemDescriptionNotFound(descriptionId.ToString()));

        itemDescription.Update(description);

        return this;
    }

    public Result<Item> RemoveDescription(Guid descriptionId)
    {
        var itemDescription = _descriptions.FirstOrDefault(x => x.Id == descriptionId);

        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        if (itemDescription is null)
            return Result.Fail<Item>(ItemErrors.ItemDescriptionNotFound(descriptionId.ToString()));

        _descriptions.Remove(itemDescription);

        return this;
    }

    public Result<Item> AddImage(string imageUri)
    {
        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        var orderNumber = _resources.FindAll(x => x.Type == imageResourceTypeName).Count + 1;

        var itemResource = ItemResource.Create(
            Id,
            imageResourceTypeName,
            string.Concat(imageResourceTypeName, "-", orderNumber),
            imageUri,
            orderNumber);

        _resources.Add(itemResource);

        return this;
    }

    public Result<Item> UpImageOrder(Guid imageId)
    {
        var itemResource = _resources.FirstOrDefault(x => x.Id == imageId);

        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        if (itemResource is null)
            return Result.Fail<Item>(ItemErrors.ItemImageNotFound(imageId.ToString()));

        if (itemResource.Order == 1)
            return Result.Fail<Item>(ItemErrors.ItemImageCannotUpdatedToUp);

        itemResource.UpOrder();

        var anotherItemResource = _resources.FirstOrDefault(x => x.Order == itemResource.Order);
        anotherItemResource?.DownOrder();

        return this;
    }

    public Result<Item> DownImageOrder(Guid imageId)
    {
        var itemResource = _resources.FirstOrDefault(x => x.Id == imageId);

        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        if (itemResource is null)
            return Result.Fail<Item>(ItemErrors.ItemImageNotFound(imageId.ToString()));

        if (itemResource.Order == _resources.Count)
            return Result.Fail<Item>(ItemErrors.ItemImageCannotUpdatedToDown);

        itemResource.DownOrder();

        var anotherItemResource = _resources.FirstOrDefault(x => x.Order == itemResource.Order);
        anotherItemResource?.UpOrder();

        return this;
    }

    public Result<Item> RemoveImage(Guid imageId)
    {
        var itemResource = _resources.FirstOrDefault(x => x.Id == imageId);

        if (Status is ItemStatus.Disabled)
            return Result.Fail<Item>(ItemErrors.ItemIsDisabled);

        if (itemResource is null)
            return Result.Fail<Item>(ItemErrors.ItemImageNotFound(imageId.ToString()));

        _resources.Remove(itemResource);

        return this;
    }
}