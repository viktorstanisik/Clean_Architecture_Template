namespace Application.UserFeature.Commands.CreateUser;

public class CreateUserCommand(UserDto userDto) : IRequest<bool>
{
    public UserDto UserDto { get; } = userDto;
}