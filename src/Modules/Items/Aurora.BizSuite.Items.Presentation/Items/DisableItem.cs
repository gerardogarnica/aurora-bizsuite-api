using Aurora.BizSuite.Items.Application.Items.UpdateStatus;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class DisableItem : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "items/disable/{id}",
            async (Guid id, ISender sender) =>
            {
                var command = new UpdateItemStatusCommand(id, false);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Accepted(string.Empty),
                    ApiResponses.Problem);
            })
            .WithName("DisableItem")
            .WithTags(EndpointTags.Item)
            .Produces<Guid>(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}