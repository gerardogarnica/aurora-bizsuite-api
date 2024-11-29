namespace Aurora.BizSuite.Items.Domain.Brands;

public interface IBrandRepository : IRepository<Brand>
{
    Task<Brand?> GetByIdAsync(BrandId id);
    Task<Brand?> GetByNameAsync(string name);
    Task<PagedResult<Brand>> GetPagedAsync(PagedViewRequest paged, string? searchTerms);
    Task InsertAsync(Brand brand);
    void Update(Brand brand);
    void Delete(Brand brand);
}