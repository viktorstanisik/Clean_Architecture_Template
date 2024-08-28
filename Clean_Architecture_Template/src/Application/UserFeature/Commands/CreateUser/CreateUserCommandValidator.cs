namespace Application.UserFeature.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.UserDto.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("A valid email is required.");

        RuleFor(x => x.UserDto.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.UserDto.City)
            .NotEmpty()
            .WithMessage("City is required.");

        RuleFor(x => x.UserDto.StreetNo)
            .NotEmpty()
            .WithMessage("Street number is required.");
    }
}