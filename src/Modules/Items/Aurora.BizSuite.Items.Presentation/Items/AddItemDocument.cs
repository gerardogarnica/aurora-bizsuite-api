using Aurora.BizSuite.Items.Application.Items.AddResource;
using Aurora.BizSuite.Items.Domain.Items;

namespace Aurora.BizSuite.Items.Presentation.Items;

internal sealed class AddItemDocument : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
            "items/documents/add/{id}",
            async (Guid id, [FromBody] AddItemDocumentRequest request, ISender sender) =>
            {
                var command = new AddItemResourceCommand(
                    id,
                    request.Uri,
                    ItemConstants.DocumentResourceTypeName,
                    request.Name);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Created(),
                    ApiResponses.Problem);
            })
            .WithName("AddItemDocument")
            .WithTags(EndpointTags.Item)
            .Produces(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal sealed record AddItemDocumentRequest(string Name, string Uri);
}