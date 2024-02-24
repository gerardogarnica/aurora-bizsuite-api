using Aurora.BizSuite.Settings.Application.Options;
using Aurora.BizSuite.Settings.Application.Options.Create;
using Aurora.BizSuite.Settings.Application.Options.GetByCode;
using Aurora.BizSuite.Settings.Application.Options.GetList;
using Aurora.BizSuite.Settings.Application.Options.Update;

namespace Aurora.BizSuite.Settings.API.Endpoints;

public class OptionsEndpoint : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("aurora/bizsuite/options")
            .WithTags("Options")
            .RequireAuthorization();

        group.MapGet("/{code}", GetByCode)
            .WithName("GetOptionByCodeQuery");

        group.MapGet("/", GetList)
            .WithName("GetOptionList");

        group.MapPost("/", Create)
            .WithName("CreateOption");

        group.MapPut("/", Update)
            .WithName("UpdateOption");
    }

    private async Task<Results<Ok<OptionModel>, NoContent>> GetByCode(
        string code,
        ISender sender)
    {
        var result = await sender.Send(new GetOptionByCodeQuery()
        {
            Code = code
        });

        return result != null
            ? TypedResults.Ok(result)
            : TypedResults.NoContent();
    }

    private async Task<Results<Ok<PagedResult<OptionModel>>, BadRequest>> GetList(
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] string? searchTerms,
        ISender sender)
    {
        var result = await sender.Send(new GetOptionListQuery()
        {
            Paged = new PagedViewRequest(page, pageSize),
            SearchTerms = searchTerms
        });

        return TypedResults.Ok(result);
    }

    private async Task<Results<Created<OptionModel>, BadRequest>> Create(
        [FromBody] CreateOptionCommand command,
        ISender sender)
    {
        var result = await sender.Send(command);

        return TypedResults.Created(string.Empty, result);
    }

    private async Task<Results<Accepted<Result>, BadRequest>> Update(
        [FromBody] UpdateOptionCommand command,
        ISender sender)
    {
        var result = await sender.Send(command);

        return TypedResults.Accepted(string.Empty, result);
    }
}