namespace Aurora.BizSuite.Security.Application.Users.Create;

public class CreateUserCommandHandler(
    IPasswordProvider passwordProvider,
    IUserRepository userRepository)
    : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        // Create password
        var passwordResult = Password.Create(request.Email);
        if (!passwordResult.IsSuccessful)
            return Result.Fail<Guid>(passwordResult.Error);

        // Create user
        var user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            passwordProvider.HashPassword(passwordResult.Value),
            DateTime.UtcNow.Date,
            request.Notes,
            request.IsEditable);

        await userRepository.InsertAsync(user);

        return user.Id.Value;
    }
}