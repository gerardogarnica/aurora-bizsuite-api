namespace Aurora.BizSuite.Security.Application.Users.UpdateStatus;

internal class UpdateUserStatusCommandHandler(
    IUserRepository userRepository)
    : ICommandHandler<UpdateUserStatusCommand>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result> Handle(
        UpdateUserStatusCommand request,
        CancellationToken cancellationToken)
    {
        // Get user
        var user = await _userRepository.GetByIdAsync(new UserId(request.Id));

        if (user is null)
            return Result.Fail(DomainErrors.UserErrors.UserNotFound(request.Id));

        // Update user status
        var userResult = request.IsActive
            ? user.Activate()
            : user.Inactivate();

        if (!userResult.IsSuccessful)
            return Result.Fail(userResult.Error);

        _userRepository.Update(userResult.Value);

        return Result.Ok();
    }
}