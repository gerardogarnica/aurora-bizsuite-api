using Aurora.BizSuite.Security.Application.Users.Create;
using Aurora.BizSuite.Security.Application.Users.GetById;
using Aurora.BizSuite.Security.Application.Users.GetList;
using Aurora.BizSuite.Security.Application.Users.Update;
using Aurora.BizSuite.Security.Application.Users.UpdateStatus;

namespace Aurora.BizSuite.Security.API.Endpoints;

public class UserEndpoints : IBaseEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup("aurora/bizsuite/users")
            .WithTags("User")
            .RequireAuthorization();

        GetUserById(group);
        GetPagedUsers(group);
        CreateUser(group);
        UpdateUser(group);
        ActivateUser(group);
        InactivateUser(group);
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
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
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
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
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
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
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
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }

    static RouteHandlerBuilder ActivateUser(RouteGroupBuilder routeGroup)
    {
        return routeGroup.MapPut(
            "/{guid}/activate",
            async (Guid guid, ISender sender) =>
            {
                var command = new UpdateUserStatusCommand(guid, true);

                var result = await sender.Send(command);

                return result.IsSuccessful
                    ? Results.Accepted(string.Empty)
                    : result.ToBadRequest();
            })
            .WithName("ActivateUser")
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }

    static RouteHandlerBuilder InactivateUser(RouteGroupBuilder routeGroup)
    {
        return routeGroup.MapPut(
            "/{guid}/inactivate",
            async (Guid guid, ISender sender) =>
            {
                var command = new UpdateUserStatusCommand(guid, false);

                var result = await sender.Send(command);

                return result.IsSuccessful
                    ? Results.Accepted(string.Empty)
                    : result.ToBadRequest();
            })
            .WithName("InactivateUser")
            .Produces(StatusCodes.Status202Accepted)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AllowAnonymous();
    }
}