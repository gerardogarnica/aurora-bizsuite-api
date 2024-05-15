namespace Aurora.BizSuite.Items.Domain.Categories;

public class Category : AggregateRoot<CategoryId>, IAuditableEntity
{
    public string Name { get; private set; }
    public CategoryId ParentId { get; private set; }
    public int LevelNumber { get; private set; }
    public string? Notes { get; private set; }
    public string? CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }

    protected Category()
        : base(new CategoryId(Guid.NewGuid()))
    {
        Name = string.Empty;
        ParentId = null!;
    }

    public static Category Create(
        string name,
        CategoryId parentId,
        int levelNumber,
        string? notes)
    {
        var category = new Category
        {
            Name = name,
            ParentId = parentId,
            LevelNumber = levelNumber,
            Notes = notes
        };

        category.AddDomainEvent(new CategoryCreatedDomainEvent(category.Id.Value));

        return category;
    }
}