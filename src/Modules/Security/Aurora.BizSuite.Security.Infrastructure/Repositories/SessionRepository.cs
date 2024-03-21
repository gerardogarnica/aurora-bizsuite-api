namespace Aurora.BizSuite.Security.Infrastructure.Repositories;

internal class SessionRepository : BaseRepository<UserSession, UserSessionId>, ISessionRepository
{
    private readonly SecurityContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public SessionRepository(SecurityContext context)
        : base(context)
    {
        _context = context;
    }
}