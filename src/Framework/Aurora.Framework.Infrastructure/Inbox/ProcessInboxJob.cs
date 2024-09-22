using Aurora.Framework.Application;
using Aurora.Framework.Infrastructure.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace Aurora.Framework.Infrastructure.Inbox;

[DisallowConcurrentExecution]
public abstract class ProcessInboxJob(
    DbContext dbContext,
    string moduleName,
    IServiceScopeFactory serviceScopeFactory,
    ILogger<ProcessInboxJob> logger) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Starting to process {moduleName} inbox messages.", moduleName);

        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        var messages = GetInboxMessages();

        foreach (InboxMessageResponse inboxMessage in messages)
        {
            Exception? exception = null;
            try
            {
                IIntegrationEvent integrationEvent = JsonConvert.DeserializeObject<IIntegrationEvent>(
                    inboxMessage.Content,
                    SerializerSettings.Instance)!;

                using IServiceScope scope = serviceScopeFactory.CreateScope();

                var handlers = scope.ServiceProvider.GetServices(
                    typeof(IIntegrationEventHandler<>).MakeGenericType(integrationEvent.GetType()));

                foreach (var handler in handlers)
                {
                    var handleMethod = handler!.GetType().GetMethod("Handle");
                    if (handleMethod != null)
                    {
                        await (Task)handleMethod.Invoke(handler, [integrationEvent, context.CancellationToken])!;
                    }
                }
            }
            catch (Exception processException)
            {
                exception = processException;

                logger.LogError(
                    processException,
                    "Exception occurred processing inbox message {id}. Details: {message}", inboxMessage.InboxId, processException.Message);
            }

            await UpdateInboxMessageAsync(inboxMessage, exception);
        }

        await transaction.CommitAsync();

        logger.LogInformation("Completed processing {moduleName} inbox messages.", moduleName);
    }

    public abstract IReadOnlyCollection<InboxMessageResponse> GetInboxMessages();

    public abstract Task UpdateInboxMessageAsync(InboxMessageResponse messageResponse, Exception? exception);

    public sealed record InboxMessageResponse(Guid InboxId, string Content);
}