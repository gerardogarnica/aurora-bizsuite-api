using Aurora.BizSuite.Items.Application.Units;
using Aurora.BizSuite.Items.Application.Units.GetById;

namespace Aurora.BizSuite.Items.Presentation.Units;

internal sealed class GetUnitById : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
            "units/{id}",
            async (Guid id, ISender sender) =>
            {
                Result<UnitOfMeasurementModel> result = await sender.Send(new GetUnitByIdQuery(id));

                return result.Match(Results.Ok, ApiResponses.Problem);
            })
            .WithName("GetUnitById")
            .WithTags(EndpointTags.Unit)
            .Produces<UnitOfMeasurementModel>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}