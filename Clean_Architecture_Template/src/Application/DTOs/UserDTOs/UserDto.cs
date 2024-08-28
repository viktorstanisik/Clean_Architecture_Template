namespace Application.DTOs.UserDTOs;

public class UserDto
{
    public required string Email { get; init; }
    public required string Password { get; init; }
    public required string City { get; init; }
    public required string StreetNo { get; init; }
}