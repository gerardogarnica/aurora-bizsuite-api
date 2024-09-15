using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Aurora.BizSuite.Items.Infrastructure.Outbox;

internal sealed class ItemsOutboxJob(
    ItemsDbContext dbContext,
    IServiceScopeFactory serviceScopeFactory,
    IDateTimeService dateTimeService,
    IOptions<OutboxOptions> options,
    ILogger<ItemsOutboxJob> logger) : ProcessOutboxJob(dbContext, "Items", serviceScopeFactory, logger)
{
    public override IReadOnlyCollection<OutboxMessageResponse> GetOutboxMessages()
    {
        string sql = $"""
                SELECT TOP {options.Value.BatchSize} OutboxId, Content
                FROM [{ItemsDbContext.DEFAULT_SCHEMA}].[OutboxMessages] WITH (UPDLOCK)
                WHERE IsProcessed = 0
                ORDER BY OccurredOnUtc
                """;

        IEnumerable<OutboxMessageResponse> messages = dbContext
            .Database
            .SqlQueryRaw<OutboxMessageResponse>(sql);

        return messages.ToList();
    }

    public override async Task UpdateOutboxMessageAsync(OutboxMessageResponse messageResponse, Exception? exception)
    {
        var processedOnUtc = new SqlParameter("ProcessedOnUtc", dateTimeService.UtcNow);
        var error = new SqlParameter("Error", exception is null ? string.Empty : exception.ToString());
        var outboxId = new SqlParameter("OutboxId", messageResponse.OutboxId);

        string sql = $"""
                UPDATE [{ItemsDbContext.DEFAULT_SCHEMA}].[OutboxMessages]
                SET IsProcessed = 1,
                    ProcessedOnUtc = @ProcessedOnUtc,
                    Error = @Error
                WHERE OutboxId = @OutboxId
                """;

        await dbContext
            .Database
            .ExecuteSqlRawAsync(sql, processedOnUtc, error, outboxId);
    }
}