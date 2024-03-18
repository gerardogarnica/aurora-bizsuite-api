namespace Aurora.BizSuite.Security.Domain.Users;

public interface IUserRepository : IRepository<User>
{
    Task InsertAsync(User user);
    void Update(User user);
    Task<User?> GetByIdAsync(UserId id);
    Task<User?> GetByEmailAsync(string email);
    Task<PagedResult<User>> GetPagedAsync(PagedViewRequest paged, string? searchTerms, bool onlyActives);
    Task<PagedResult<User>> GetPagedAsync(PagedViewRequest paged, Guid roleId, string? searchTerms, bool onlyActives);
}