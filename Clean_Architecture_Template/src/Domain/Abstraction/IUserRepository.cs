namespace Domain.Abstraction;

public interface IUserRepository
{
    Task<bool> CreateUser(User user);
    Task<User> Login(User user);
    Task<User> GetUser(string password);
}