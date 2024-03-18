namespace Aurora.BizSuite.Security.Domain.Session;

public interface ISessionRepository : IRepository<UserSession>
{
    Task InsertAsync(UserSession session);
    void Update(UserSession session);
}