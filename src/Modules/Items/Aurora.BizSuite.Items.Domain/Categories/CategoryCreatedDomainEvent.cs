namespace Aurora.BizSuite.Items.Domain.Categories;

public sealed class CategoryCreatedDomainEvent(
    Guid categoryId)
    : DomainEvent
{
    public Guid CategoryId { get; init; } = categoryId;
}