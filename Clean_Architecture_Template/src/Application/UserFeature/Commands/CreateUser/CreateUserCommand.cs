namespace Application.UserFeature.Commands.CreateUser;

public class CreateUserCommand(UserDto userDto) : IRequest<Result<bool>>
{
    public UserDto UserDto { get; } = userDto;
}