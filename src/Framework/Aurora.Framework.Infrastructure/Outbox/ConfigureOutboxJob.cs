using Microsoft.Extensions.Options;
using Quartz;

namespace Aurora.Framework.Infrastructure.Outbox;

public sealed class ConfigureOutboxJob(
    IOptions<OutboxOptions> options) : IConfigureOptions<QuartzOptions>
{
    private readonly OutboxOptions _outboxOptions = options.Value;

    public void Configure(QuartzOptions options)
    {
        var jobName = typeof(ProcessOutboxJob).FullName!;

        options
            .AddJob<ProcessOutboxJob>(configure => configure.WithIdentity(jobName))
            .AddTrigger(configure =>
                configure
                    .ForJob(jobName)
                    .WithSimpleSchedule(a => a
                        .WithIntervalInSeconds(_outboxOptions.IntervalInSeconds)
                        .RepeatForever()));
    }
}