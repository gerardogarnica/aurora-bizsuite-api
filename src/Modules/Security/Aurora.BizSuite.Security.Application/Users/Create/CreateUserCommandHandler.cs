namespace Aurora.BizSuite.Security.Application.Users.Create;

public class CreateUserCommandHandler(
    IPasswordProvider passwordProvider,
    IUserRepository userRepository)
    : ICommandHandler<CreateUserCommand, Guid>
{
    private readonly IPasswordProvider _passwordProvider = passwordProvider;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<Guid>> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        // Create password
        var passwordHash = _passwordProvider.HashPassword(request.Email);
        var passwordResult = Password.Create(passwordHash);
        if (!passwordResult.IsSuccessful)
            return Result.Fail<Guid>(passwordResult.Error);

        // Create user
        var user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            passwordResult.Value,
            DateTime.UtcNow.Date,
            request.Notes,
            request.IsEditable);

        await _userRepository.InsertAsync(user);

        return user.Id.Value;
    }
}