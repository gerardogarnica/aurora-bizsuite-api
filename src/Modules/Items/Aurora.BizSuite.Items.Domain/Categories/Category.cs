﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Aurora.BizSuite.Items.Domain.Categories;

public sealed class Category : AggregateRoot<CategoryId>, IAuditableEntity
{
    const int codeDigitsPerLevel = 2;
    const int maxNumberOfLevels = 4;

    private readonly List<Category> _childs = [];

    public string Name { get; private set; }
    public string Code { get; private set; }
    public CategoryId? ParentId { get; private set; }
    public int Level { get; private set; }
    public string? Notes { get; private set; }
    public bool IsLocked { get; private set; }
    [NotMapped]
    public bool IsLeaf => Level == maxNumberOfLevels;
    public string? CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public IReadOnlyCollection<Category> Childs => _childs.AsReadOnly();

    private Category() : base(new CategoryId(Guid.NewGuid()))
    {
        Name = string.Empty;
        Code = string.Empty;
        ParentId = null;
    }

    public static Category Create(
        string name,
        string? notes,
        int firstLevelCount)
    {
        var category = new Category
        {
            Name = name.Trim(),
            Code = (firstLevelCount + 1).ToString().PadLeft(codeDigitsPerLevel, '0'),
            ParentId = null,
            Level = 1,
            Notes = notes?.Trim()
        };

        category.AddDomainEvent(new CategoryCreatedDomainEvent(category.Id.Value));

        return category;
    }

    public Result<Category> Update(
        string name,
        string? notes)
    {
        Name = name.Trim();
        Notes = notes?.Trim();

        return this;
    }

    public Result<Category> Lock()
    {
        if (IsLocked)
            return Result.Fail<Category>(CategoryErrors.CategoryIsAlreadyLocked);

        IsLocked = true;

        return this;
    }

    public Result<Category> AddChild(
        string name,
        string? notes)
    {
        if (IsLocked)
            return Result.Fail<Category>(CategoryErrors.UnableToAddChildToLockedCategory);

        if (Level >= maxNumberOfLevels)
            return Result.Fail<Category>(CategoryErrors.MaxNumberOfLevelsReached);

        var category = new Category
        {
            Name = name.Trim(),
            Code = $"{Code}{(_childs.Count + 1).ToString().PadLeft(codeDigitsPerLevel, '0')}",
            ParentId = Id,
            Level = Level + 1,
            Notes = notes?.Trim()
        };

        _childs.Add(category);

        return this;
    }
}