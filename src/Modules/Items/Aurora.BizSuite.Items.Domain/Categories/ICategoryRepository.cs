﻿namespace Aurora.BizSuite.Items.Domain.Categories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByIdAsync(CategoryId id);
    Task<IReadOnlyCollection<Category>> GetListAsync(Guid? parentId, string? searchTerms);
    Task InsertAsync(Category category);
    void Update(Category category);
}