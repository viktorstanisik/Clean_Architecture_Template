namespace Domain.Entities;

public sealed class User(string email, string password, Address address)
{
    private User() : this("", "", new Address("", "")) { } // Parameterless constructor for EF Core

    public UserId Id { get; } = new(Guid.NewGuid());

    public string Email { get; private set; } = email ?? throw new ArgumentNullException(nameof(email));

    public string Password { get; private set; } = password ?? throw new ArgumentNullException(nameof(password));

    public Address Address { get; private set; } = address ?? throw new ArgumentNullException(nameof(address));

    public string UpdateUserPassword(string password)
    {
        return Password = password;
    }

    public void UpdateAddress(Address newAddress)
    {
        Address = newAddress ?? throw new ArgumentNullException(nameof(newAddress));
    }
}