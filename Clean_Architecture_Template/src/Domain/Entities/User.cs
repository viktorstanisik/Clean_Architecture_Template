using Domain.Value_Objects;

namespace Domain.Entities;

public sealed class User(string email, string password, Address address)
{
    private User() : this("", "", new Address("", "")) { } // Parameterless constructor for EF Core

    public Guid Id { get; private set; } = SetUserId();

    public string Email { get; private set; } = email ?? throw new ArgumentNullException(nameof(email));

    public string Password { get; private set; } = password ?? throw new ArgumentNullException(nameof(password));

    public Address Address { get; private set; } = address ?? throw new ArgumentNullException(nameof(address));

    private static Guid SetUserId() => Guid.NewGuid();
    public string UpdateUserPassword(string password) => Password = password;

    public void UpdateAddress(Address newAddress)
    {
        Address = newAddress ?? throw new ArgumentNullException(nameof(newAddress));
    }
}