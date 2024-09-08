using Application.DTOs.UserDTOs;
using Application.Mapper;
using Application.UserFeature.Commands.CreateUser;
using Domain.Common;
using Domain.Entities;
using Domain.ValueObjects;
using NSubstitute;

namespace Clean_Architecture_Template_Tests.UserFeatureTests;

public class CreateUserTests
{
    private readonly CreateUserCommandHandler _handler;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;

    public CreateUserTests()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _passwordHasher = Substitute.For<IPasswordHasher>();

        _handler = new CreateUserCommandHandler(_userRepository, _passwordHasher);

        MapperConfiguration.ConfigureMappings();
    }


    [Fact]
    public async Task Handle_Should_ReturnFailure_WhenUserAlreadyExists()
    {
        // Arrange
        var userDto = CreateUserDto();

        var command = new CreateUserCommand(userDto);

        _userRepository.GetUserByEmail(userDto.Email)
            .Returns(Result<User>.CreateSuccess(new User(userDto.Email, userDto.Password,
                new Address(userDto.City, userDto.StreetNo))));

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Success.Should().BeFalse();
        result.ErrorMessage.Should().Be("User already exists.");
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenUserIsCreated()
    {
        // Arrange
        var userDto = CreateUserDto();

        var command = new CreateUserCommand(userDto);

        _userRepository.GetUserByEmail(userDto.Email)
            .Returns(Result<User>.CreateFailure("User does not exist."));

        _passwordHasher.GenerateHash(userDto.Password)
            .Returns("hashedPassword");

        _userRepository.CreateUser(Arg.Any<User>())
            .Returns(true);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Success.Should()
            .BeTrue();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailure_OnException()
    {
        // Arrange
        var userDto = CreateUserDto();
        var command = new CreateUserCommand(userDto);

        _userRepository.When(x => x.GetUserByEmail(userDto.Email))
            .Do(x => throw new Exception("Unexpected error"));

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Success.Should()
            .BeFalse();

        result.ErrorMessage.Should().Be("Unexpected error");
    }

    private static UserDto CreateUserDto()
    {
        return new UserDto
        {
            Email = "test@example.com",
            Password = "password123",
            City = "TestCity",
            StreetNo = "123"
        };
    }
}