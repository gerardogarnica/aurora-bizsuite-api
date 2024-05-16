namespace Aurora.BizSuite.Items.Infrastructure.Categories;

internal sealed class CategoryRepository(
    ItemsDbContext dbContext)
    : BaseRepository<Category, CategoryId>(dbContext), ICategoryRepository
{
    public IUnitOfWork UnitOfWork => dbContext;
}