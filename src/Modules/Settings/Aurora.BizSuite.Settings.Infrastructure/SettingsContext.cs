namespace Aurora.BizSuite.Settings.Infrastructure;

public class SettingsContext : DbContext, IUnitOfWork
{
    #region DbSet properties

    public DbSet<Option> Options { get; set; } = null!;
    public DbSet<OptionItem> OptionItems { get; set; } = null!;

    #endregion

    internal const string DEFAULT_SCHEMA = "SET";

    private readonly IPublisher _publisher;

    public SettingsContext(
        DbContextOptions<SettingsContext> options,
        IPublisher publisher)
        : base(options)
    {
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SettingsContext).Assembly);
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

    public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}