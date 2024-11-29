using Aurora.BizSuite.Items.Application.Brands.Delete;

namespace Aurora.BizSuite.Items.Presentation.Brands;

internal sealed class DeleteBrand : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(
            "brands/{id}",
            async (Guid id, ISender sender) =>
            {
                var command = new DeleteBrandCommand(id);

                Result result = await sender.Send(command);

                return result.Match(
                    () => Results.Accepted(string.Empty),
                    ApiResponses.Problem);
            })
            .WithName("DeleteBrand")
            .WithTags(EndpointTags.Brand)
            .Produces<Guid>(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}