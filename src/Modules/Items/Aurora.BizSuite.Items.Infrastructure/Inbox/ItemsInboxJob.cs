using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Aurora.BizSuite.Items.Infrastructure.Inbox;

internal sealed class ItemsInboxJob(
    ItemsDbContext dbContext,
    IServiceScopeFactory serviceScopeFactory,
    IDateTimeService dateTimeService,
    IOptions<InboxOptions> options,
    ILogger<ItemsInboxJob> logger) : ProcessInboxJob(dbContext, "Items", serviceScopeFactory, logger)
{
    public override IReadOnlyCollection<InboxMessageResponse> GetInboxMessages()
    {
        string sql = $"""
                SELECT TOP {options.Value.BatchSize} InboxId, Content
                FROM [{ItemsDbContext.DEFAULT_SCHEMA}].[InboxMessages] WITH (UPDLOCK)
                WHERE IsProcessed = 0
                ORDER BY OccurredOnUtc
                """;

        IEnumerable<InboxMessageResponse> messages = dbContext
            .Database
            .SqlQueryRaw<InboxMessageResponse>(sql);

        return messages.ToList();
    }

    public override async Task UpdateInboxMessageAsync(InboxMessageResponse messageResponse, Exception? exception)
    {
        var processedOnUtc = new SqlParameter("ProcessedOnUtc", dateTimeService.UtcNow);
        var error = new SqlParameter("Error", exception is null ? string.Empty : exception.ToString());
        var inboxId = new SqlParameter("InboxId", messageResponse.InboxId);

        string sql = $"""
                UPDATE [{ItemsDbContext.DEFAULT_SCHEMA}].[InboxMessages]
                SET IsProcessed = 1,
                    ProcessedOnUtc = @ProcessedOnUtc,
                    Error = @Error
                WHERE InboxId = @InboxId
                """;

        await dbContext
            .Database
            .ExecuteSqlRawAsync(sql, processedOnUtc, error, inboxId);
    }
}