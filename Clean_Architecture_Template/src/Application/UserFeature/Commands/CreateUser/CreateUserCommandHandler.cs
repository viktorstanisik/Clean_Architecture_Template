
namespace Application.UserFeature.Commands.CreateUser;
internal sealed class CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    : IRequestHandler<CreateUserCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            Result<User> userAlreadyExist = await userRepository.GetUserByEmail(request.UserDto.Email);

            if (userAlreadyExist.Success)
            {
                return Result<bool>.CreateFailure("User already exists.");
            }

            // Map DTO to domain entity
            var user = request.UserDto.Adapt<User>();

            // Hash the password before updating the user entity
            user.UpdateUserPassword(passwordHasher.GenerateHash(request.UserDto.Password));

            var userCreated = await userRepository.CreateUser(user);

            return Result<bool>.CreateSuccess(userCreated);
        }
        catch (Exception e)
        {
            return Result<bool>.CreateFailure(e.Message);

        }
    }
}
