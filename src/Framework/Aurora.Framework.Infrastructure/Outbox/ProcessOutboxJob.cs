using Aurora.Framework.Infrastructure.Serialization;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace Aurora.Framework.Infrastructure.Outbox;

[DisallowConcurrentExecution]
public abstract class ProcessOutboxJob(
    DbContext dbContext,
    string moduleName,
    IServiceScopeFactory serviceScopeFactory,
    ILogger<ProcessOutboxJob> logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Starting to process {moduleName} outbox messages.", moduleName);

        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        var messages = GetOutboxMessages();

        foreach (OutboxMessageResponse outboxMessage in messages)
        {
            Exception? exception = null;
            try
            {
                IDomainEvent domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    SerializerSettings.Instance)!;

                using IServiceScope scope = serviceScopeFactory.CreateScope();

                IPublisher publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

                await publisher.Publish(domainEvent);
            }
            catch (Exception processException)
            {
                exception = processException;

                logger.LogError(
                    processException,
                    "Exception occurred processing outbox message {id}. Details: {message}", outboxMessage.OutboxId, processException.Message);
            }

            await UpdateOutboxMessageAsync(outboxMessage, exception);
        }

        await transaction.CommitAsync();

        logger.LogInformation("Completed processing {moduleName} outbox messages.", moduleName);
    }

    public abstract IReadOnlyCollection<OutboxMessageResponse> GetOutboxMessages();

    public abstract Task UpdateOutboxMessageAsync(OutboxMessageResponse messageResponse, Exception? exception);

    public sealed record OutboxMessageResponse(Guid OutboxId, string Content);
}