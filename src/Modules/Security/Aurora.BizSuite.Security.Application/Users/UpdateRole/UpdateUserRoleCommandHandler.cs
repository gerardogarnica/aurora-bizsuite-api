namespace Aurora.BizSuite.Security.Application.Users.UpdateRole;

internal class UpdateUserRoleCommandHandler(
    IUserRepository userRepository,
    IRoleRepository roleRepository)
    : ICommandHandler<UpdateUserRoleCommand>
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IRoleRepository _roleRepository = roleRepository;

    public async Task<Result> Handle(
        UpdateUserRoleCommand request,
        CancellationToken cancellationToken)
    {
        // Get user
        var user = await _userRepository.GetByIdAsync(new UserId(request.UserId));

        if (user is null)
            return Result.Fail(DomainErrors.UserErrors.UserNotFound(request.UserId));

        // Get role
        var role = await _roleRepository.GetByIdAsync(new RoleId(request.RoleId));

        if (role is null)
            return Result.Fail(DomainErrors.RoleErrors.RoleNotFound(request.RoleId));

        // Update user role
        var userResult = request.IsActive
            ? user.AddRole(role, false)
            : user.RemoveRole(role);

        if (!userResult.IsSuccessful)
            return Result.Fail(userResult.Error);

        _userRepository.Update(userResult.Value);

        return Result.Ok();
    }
}