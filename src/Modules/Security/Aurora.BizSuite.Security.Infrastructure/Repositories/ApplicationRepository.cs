using ApplicationId = Aurora.BizSuite.Security.Domain.Applications.ApplicationId;

namespace Aurora.BizSuite.Security.Infrastructure.Repositories;

internal class ApplicationRepository(SecurityContext context)
    : BaseRepository<Application, ApplicationId>(context), IApplicationRepository
{
    public IUnitOfWork UnitOfWork => context;

    public async Task<IList<Application>> GetListAsync() => await context
        .Applications
        .OrderBy(x => x.Name)
        .ToListAsync();
}