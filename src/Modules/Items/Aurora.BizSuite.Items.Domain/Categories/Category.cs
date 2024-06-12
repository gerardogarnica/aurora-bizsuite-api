namespace Aurora.BizSuite.Items.Domain.Categories;

public sealed class Category : AggregateRoot<CategoryId>, IAuditableEntity
{
    private readonly List<Category> _childs = [];

    public string Name { get; private set; }
    public CategoryId? ParentId { get; private set; }
    public int Level { get; private set; }
    public string? Notes { get; private set; }
    public string? CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public string? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
    public IReadOnlyCollection<Category> Childs => _childs.AsReadOnly();

    private Category() : base(new CategoryId(Guid.NewGuid()))
    {
        Name = string.Empty;
        ParentId = null;
    }

    public static Category Create(
        string name,
        string? notes)
    {
        var category = new Category
        {
            Name = name.Trim(),
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

    public Result<Category> AddChild(
        string name,
        string? notes)
    {
        var category = new Category
        {
            Name = name.Trim(),
            ParentId = Id,
            Level = Level + 1,
            Notes = notes?.Trim()
        };

        _childs.Add(category);

        return this;
    }
}