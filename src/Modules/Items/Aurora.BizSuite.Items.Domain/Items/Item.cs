﻿using Aurora.BizSuite.Items.Domain.Categories;
using Aurora.BizSuite.Items.Domain.Units;

namespace Aurora.BizSuite.Items.Domain.Items;

public class Item : AggregateRoot<ItemId>, IAuditableEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public ItemType ItemType { get; private set; }
    public UnitOfMeasurementId MainUnitId { get; private set; }
    public string? AlternCode { get; private set; }
    public string? Notes { get; private set; }
    public string? CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public Category Category { get; init; } = null!;
    public UnitOfMeasurement MainUnit { get; init; } = null!;

    protected Item()
        : base(new ItemId(Guid.NewGuid()))
    {
        Name = string.Empty;
        Description = string.Empty;
        CategoryId = null!;
        MainUnitId = null!;
    }

    public static Item Create(
        string name,
        string description,
        Category category,
        ItemType itemType,
        UnitOfMeasurement mainUnit,
        string? alternCode,
        string? notes)
    {
        var item = new Item
        {
            Name = name,
            Description = description,
            CategoryId = category.Id,
            ItemType = itemType,
            MainUnitId = mainUnit.Id,
            AlternCode = alternCode,
            Notes = notes
        };

        item.AddDomainEvent(new ItemCreatedDomainEvent(item.Id.Value));

        return item;
    }
}