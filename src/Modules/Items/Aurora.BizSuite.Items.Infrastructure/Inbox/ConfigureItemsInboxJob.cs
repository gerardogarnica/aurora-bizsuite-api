namespace Aurora.BizSuite.Items.Infrastructure.Inbox;

internal sealed class ConfigureItemsInboxJob(
    IOptions<InboxOptions> options) : IConfigureOptions<QuartzOptions>
{
    private readonly InboxOptions _inboxOptions = options.Value;

    public void Configure(QuartzOptions options)
    {
        var jobName = typeof(ItemsInboxJob).FullName!;

        options
            .AddJob<ItemsInboxJob>(configure => configure.WithIdentity(jobName))
            .AddTrigger(configure =>
                configure
                    .ForJob(jobName)
                    .WithSimpleSchedule(a => a
                        .WithIntervalInSeconds(_inboxOptions.IntervalInSeconds)
                        .RepeatForever()));
    }
}