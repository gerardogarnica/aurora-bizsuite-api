using Aurora.Framework.Application;
using Aurora.Framework.Application.EventBus;
using Aurora.Framework.Infrastructure.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using System.Collections.Concurrent;
using System.Reflection;

namespace Aurora.Framework.Infrastructure.Inbox;

[DisallowConcurrentExecution]
public abstract class ProcessInboxJob(
    DbContext dbContext,
    string moduleName,
    Assembly presentationAssembly,
    IServiceScopeFactory serviceScopeFactory,
    ILogger<ProcessInboxJob> logger) : IJob
{
    private static readonly ConcurrentDictionary<string, Type[]> HandlersDictionary = new();

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

                IEnumerable<IIntegrationEventHandler> handlers = GetHandlers(integrationEvent.GetType(), scope.ServiceProvider);
                foreach (IIntegrationEventHandler handler in handlers)
                {
                    await handler.Handle(integrationEvent, context.CancellationToken);
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

    private IEnumerable<IIntegrationEventHandler> GetHandlers(Type integrationEventType, IServiceProvider serviceProvider)
    {
        Type[] integrationEventHandlerTypes = HandlersDictionary.GetOrAdd(
            $"{presentationAssembly.GetName().Name}-{integrationEventType.Name}",
            _ =>
            {
                Type[] integrationEventHandlers = presentationAssembly
                    .GetTypes()
                    .Where(x => x.IsAssignableTo(typeof(IIntegrationEventHandler<>).MakeGenericType(integrationEventType)))
                    .ToArray();

                return integrationEventHandlers;
            });

        List<IIntegrationEventHandler> handlers = [];
        foreach (Type integrationEventHandlerType in integrationEventHandlerTypes)
        {
            object integrationEventHandler = serviceProvider.GetRequiredService(integrationEventHandlerType);

            handlers.Add((integrationEventHandler as IIntegrationEventHandler)!);
        }

        return handlers;
    }
}