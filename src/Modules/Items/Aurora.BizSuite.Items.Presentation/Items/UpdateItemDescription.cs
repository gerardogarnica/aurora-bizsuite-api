using Aurora.BizSuite.Items.Application.Items.UpdateDescription;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class UpdateItemDescription : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut(
            "items/descriptions/update/{id}",
            async (Guid id, [FromBody] UpdateItemDescriptionRequest request, ISender sender) =>
            {
                var command = new UpdateItemDescriptionCommand(
                    id,
                    request.ItemDescriptionId,
                    request.Description);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Accepted(string.Empty),
                    ApiResponses.Problem);
            })
            .WithName("UpdateItemDescription")
            .WithTags(EndpointTags.Item)
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record UpdateItemDescriptionRequest(
        Guid ItemDescriptionId,
        string Description);
}