namespace Clean_Architecture_Template_Application.UserFeature.Commands.CreateUser;


public class CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    : IRequestHandler<CreateUserCommand, bool>
{
    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = request.UserDto.Adapt<User>();

        user.UpdateUserPassword(passwordHasher.GenerateHash(request.UserDto.Password));

        var userCreated = await userRepository.CreateUser(user);

        return userCreated;
    }
}