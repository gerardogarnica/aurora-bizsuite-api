namespace Aurora.BizSuite.Security.Domain.Applications;

public interface IApplicationRepository : IRepository<Application>
{
    Task<Application?> GetByIdAsync(ApplicationId id);
    Task<IList<Application>> GetListAsync();
}