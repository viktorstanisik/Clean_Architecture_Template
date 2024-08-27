namespace Clean_Architecture_Template_Application.UserFeature.Commands.CreateUser;

public class CreateUserCommand(UserDto userDto) : IRequest<bool>
{
    public UserDto UserDto { get; } = userDto;
}