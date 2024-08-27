using Clean_Architecture_Template_Domain.Entities;

namespace Clean_Architecture_Template_Domain.Abstraction;

public interface IUserRepository
{
    Task<bool> CreateUser(User user);
    Task<User> Login(User user);
    Task<User> GetUser(string password);
}