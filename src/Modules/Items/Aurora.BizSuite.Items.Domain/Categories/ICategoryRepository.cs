namespace Aurora.BizSuite.Items.Domain.Categories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByIdAsync(CategoryId id);
}