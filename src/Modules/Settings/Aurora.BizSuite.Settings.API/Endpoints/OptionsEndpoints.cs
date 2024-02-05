using Aurora.BizSuite.Settings.Application.Options;
using Aurora.BizSuite.Settings.Application.Options.Create;
using Aurora.BizSuite.Settings.Application.Options.GetByCode;
using Aurora.BizSuite.Settings.Application.Options.GetList;
using Aurora.BizSuite.Settings.Application.Options.Update;
using Microsoft.AspNetCore.Mvc;

namespace Aurora.BizSuite.Settings.API.Endpoints;

public class OptionsEndpoints : IBaseEndpoints
{
    public void AddRoutes(WebApplication app)
    {
        var group = app.MapGroup("aurora/bizsuite/options");

        group.MapGet("/{code}", GetByCode)
            .WithName("GetOptionByCodeQuery")
            .WithOpenApi();

        group.MapGet("/", GetList)
            .WithName("GetOptionList")
            .WithOpenApi();

        group.MapPost("/", Create)
            .WithName("CreateOption")
            .WithOpenApi();

        group.MapPut("/", Update)
            .WithName("UpdateOption")
            .WithOpenApi();
    }

    private async Task<Results<Ok<OptionModel>, NotFound>> GetByCode(
        string code,
        ISender sender)
    {
        var result = await sender.Send(new GetOptionByCodeQuery()
        {
            Code = code
        });

        return result != null
            ? TypedResults.Ok(result)
            : TypedResults.NotFound();
    }

    private async Task<Results<Ok<PagedResult<OptionModel>>, BadRequest>> GetList(
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] string? searchCriteria,
        ISender sender)
    {
        var result = await sender.Send(new GetOptionListQuery()
        {
            Paged = new PagedViewRequest(page, pageSize),
            SearchCriteria = searchCriteria
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