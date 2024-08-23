using Aurora.BizSuite.Items.Application.Items.UpdateStatus;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class EnableItem : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "items/enable/{id}",
            async (Guid id, ISender sender) =>
            {
                var command = new UpdateItemStatusCommand(id, true);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Accepted(string.Empty),
                    ApiResponses.Problem);
            })
            .WithName("EnableItem")
            .WithTags(EndpointTags.Item)
            .Produces<Guid>(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}