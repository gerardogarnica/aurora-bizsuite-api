namespace Aurora.BizSuite.Settings.Domain.Options;

public interface IOptionRepository : IRepository<Option>
{
    Task InsertAsync(Option option);
    void Update(Option option);
    Task<Option?> GetOptionAsync(string code);
    Task<PagedResult<Option>> GetPagedOptionAsync(PagedViewRequest paged, string? searchCriteria);
}