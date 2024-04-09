namespace Aurora.BizSuite.Security.Infrastructure.Repositories;

internal class SessionRepository(SecurityContext context)
    : BaseRepository<UserSession, UserSessionId>(context), ISessionRepository
{
    public IUnitOfWork UnitOfWork => context;
}