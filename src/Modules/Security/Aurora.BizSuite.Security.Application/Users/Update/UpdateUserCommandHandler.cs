namespace Aurora.BizSuite.Security.Application.Users.Update;

internal class UpdateUserCommandHandler(
    IUserRepository userRepository)
    : ICommandHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        // Get user
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null)
            return Result.Fail(DomainErrors.UserErrors.UserNotFound(request.Email));

        // Update user
        var userResult = user.Update(
            request.FirstName,
            request.LastName,
            request.Notes);

        if (!userResult.IsSuccessful)
            return Result.Fail(userResult.Error);

        _userRepository.Update(userResult.Value);

        return Result.Ok();
    }
}