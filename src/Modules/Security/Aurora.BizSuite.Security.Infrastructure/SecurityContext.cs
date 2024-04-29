namespace Aurora.BizSuite.Security.Infrastructure;

public class SecurityContext : DbContext, IUnitOfWork
{
    #region DbSet properties

    public DbSet<Application> Applications { get; set; } = null!;
    public DbSet<Role> Roles { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserRole> UserRoles { get; set; } = null!;
    public DbSet<UserSession> UserSessions { get; set; } = null!;

    #endregion

    internal const string DEFAULT_SCHEMA = "SEC";
    internal const string BATCH_USER = "BATCH-USR";

    private readonly IPublisher _publisher;
    internal readonly IPasswordProvider PasswordProvider;

    public SecurityContext(
        DbContextOptions<SecurityContext> options,
        IPublisher publisher,
        IPasswordProvider pwdProvider)
        : base(options)
    {
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        PasswordProvider = pwdProvider ?? throw new ArgumentNullException(nameof(pwdProvider));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SecurityContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = ChangeTracker
            .Entries<IAggregateRoot>()
            .Select(x => x.Entity)
            .Where(x => x.DomainEvents.Count != 0)
            .SelectMany(x =>
            {
                var domainEvents = x.DomainEvents;
                x.ClearDomainEvents();
                return domainEvents;
            })
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
            await _publisher.Publish(domainEvent, cancellationToken);

        return result;
    }
}