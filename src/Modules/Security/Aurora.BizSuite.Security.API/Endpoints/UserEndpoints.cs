using Aurora.BizSuite.Security.Application.Users.Create;
using Aurora.BizSuite.Security.Application.Users.GetById;
using Aurora.BizSuite.Security.Application.Users.GetList;
using Aurora.BizSuite.Security.Application.Users.Update;
using Aurora.BizSuite.Security.Application.Users.UpdateRole;
using Aurora.BizSuite.Security.Application.Users.UpdateStatus;

namespace Aurora.BizSuite.Security.API.Endpoints;

public class UserEndpoints : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("aurora/bizsuite/users")
            .WithTags("User")
            .WithOpenApi()
            .RequireAuthorization();

        GetUserById(group);
        GetPagedUsers(group);
        CreateUser(group);
        UpdateUser(group);
        ActivateUser(group);
        InactivateUser(group);
        AddRole(group);
        RemoveRole(group);
    }

    static RouteHandlerBuilder GetUserById(RouteGroupBuilder routeGroup)
    {
        return routeGroup.MapGet(
            "/{id}",
            async (Guid id, ISender sender) =>
            {
                var query = new GetUserByIdQuery(id);

                var result = await sender.Send(query);

                return result.IsSuccessful
                    ? Results.Ok(result.Value)
                    : Results.NoContent();
            })
            .WithName("GetUserById")
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    static RouteHandlerBuilder GetPagedUsers(RouteGroupBuilder routeGroup)
    {
        return routeGroup.MapGet(
            "/",
            async (
                [FromQuery] int page,
                [FromQuery] int pageSize,
                [FromQuery] Guid? roleId,
                [FromQuery] string? searchTerms,
                [FromQuery] bool onlyActives,
                ISender sender) =>
            {
                var query = new GetUserListQuery(
                    new Framework.PagedViewRequest(page, pageSize),
                    roleId,
                    searchTerms,
                    onlyActives);

                var result = await sender.Send(query);

                return result.IsSuccessful
                    ? Results.Ok(result.Value)
                    : Results.NoContent();
            })
            .WithName("GetPagedUsers")
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    static RouteHandlerBuilder CreateUser(RouteGroupBuilder routeGroup)
    {
        return routeGroup.MapPost(
            "/create",
            async ([FromBody] CreateUserCommand request, ISender sender) =>
            {
                var command = new CreateUserCommand(
                    request.Email,
                    request.FirstName,
                    request.LastName,
                    request.Notes,
                    request.IsEditable);

                var result = await sender.Send(command);

                return result.IsSuccessful
                    ? Results.Created(string.Empty, result.Value)
                    : result.ToBadRequest();
            })
            .WithName("CreateUser")
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    static RouteHandlerBuilder UpdateUser(RouteGroupBuilder routeGroup)
    {
        return routeGroup.MapPut(
            "/update",
            async ([FromBody] UpdateUserCommand request, ISender sender) =>
            {
                var command = new UpdateUserCommand(
                    request.Email,
                    request.FirstName,
                    request.LastName,
                    request.Notes);

                var result = await sender.Send(command);

                return result.IsSuccessful
                    ? Results.Accepted(string.Empty)
                    : result.ToBadRequest();
            })
            .WithName("UpdateUser")
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    static RouteHandlerBuilder ActivateUser(RouteGroupBuilder routeGroup)
    {
        return routeGroup.MapPut(
            "/{userId}/activate",
            async (Guid userId, ISender sender) =>
            {
                var command = new UpdateUserStatusCommand(userId, true);

                var result = await sender.Send(command);

                return result.IsSuccessful
                    ? Results.Accepted(string.Empty)
                    : result.ToBadRequest();
            })
            .WithName("ActivateUser")
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    static RouteHandlerBuilder InactivateUser(RouteGroupBuilder routeGroup)
    {
        return routeGroup.MapPut(
            "/{userId}/inactivate",
            async (Guid userId, ISender sender) =>
            {
                var command = new UpdateUserStatusCommand(userId, false);

                var result = await sender.Send(command);

                return result.IsSuccessful
                    ? Results.Accepted(string.Empty)
                    : result.ToBadRequest();
            })
            .WithName("InactivateUser")
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    static RouteHandlerBuilder AddRole(RouteGroupBuilder routeGroup)
    {
        return routeGroup.MapPut(
            "/{userId}/roles/add/{roleId}",
            async (Guid userId, Guid roleId, ISender sender) =>
            {
                var command = new UpdateUserRoleCommand(userId, roleId, true);

                var result = await sender.Send(command);

                return result.IsSuccessful
                    ? Results.Accepted(string.Empty)
                    : result.ToBadRequest();
            })
            .WithName("AddRole")
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }

    static RouteHandlerBuilder RemoveRole(RouteGroupBuilder routeGroup)
    {
        return routeGroup.MapPut(
            "/{userId}/roles/remove/{roleId}",
            async (Guid userId, Guid roleId, ISender sender) =>
            {
                var command = new UpdateUserRoleCommand(userId, roleId, false);

                var result = await sender.Send(command);

                return result.IsSuccessful
                    ? Results.Accepted(string.Empty)
                    : result.ToBadRequest();
            })
            .WithName("RemoveRole")
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);
    }
}