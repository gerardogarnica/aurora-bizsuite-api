namespace Aurora.BizSuite.Items.Infrastructure.Outbox;

internal sealed class ConfigureItemsOutboxJob(
    IOptions<OutboxOptions> options) : IConfigureOptions<QuartzOptions>
{
    private readonly OutboxOptions _outboxOptions = options.Value;

    public void Configure(QuartzOptions options)
    {
        var jobName = typeof(ItemsOutboxJob).FullName!;

        options
            .AddJob<ItemsOutboxJob>(configure => configure.WithIdentity(jobName))
            .AddTrigger(configure =>
                configure
                    .ForJob(jobName)
                    .WithSimpleSchedule(a => a
                        .WithIntervalInSeconds(_outboxOptions.IntervalInSeconds)
                        .RepeatForever()));
    }
}