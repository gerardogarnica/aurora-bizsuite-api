﻿using Aurora.BizSuite.Security.Application.Applications.GetList;
using Aurora.BizSuite.Security.Application.Roles.GetById;

namespace Aurora.BizSuite.Security.API.Endpoints;

public class RoleEndpoints : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("aurora/bizsuite/roles")
            .WithTags("Role")
            .WithOpenApi()
            .RequireAuthorization();

        group.GetRoleById();
    }
}

internal static class RoleEndpointsExtensions
{
    internal static RouteHandlerBuilder GetRoleById(this RouteGroupBuilder routeGroup)
    {
        return routeGroup.MapGet(
            "/{id}",
            async (Guid id, ISender sender) =>
            {
                var query = new GetRoleByIdQuery(id);

                var result = await sender.Send(query);

                return result.IsSuccessful
                    ? Results.Ok(result.Value)
                    : Results.NoContent();
            })
            .WithName("GetRoleById")
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    internal static RouteHandlerBuilder GetApplicationList(RouteGroupBuilder routeGroup)
    {
        return routeGroup.MapGet(
            "/applications",
            async (ISender sender) =>
            {
                var query = new GetApplicationListQuery();

                var result = await sender.Send(query);

                return result.IsSuccessful
                    ? Results.Ok(result.Value)
                    : Results.NoContent();
            })
            .WithName("GetApplicationList")
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }