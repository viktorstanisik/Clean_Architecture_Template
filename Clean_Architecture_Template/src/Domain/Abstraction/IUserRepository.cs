using Domain.Common;

namespace Domain.Abstraction;

public interface IUserRepository
{
    Task<bool> CreateUser(User user);
    Task<Result<User>> Login(User user);
    Task<Result<User>> GetUserByEmail(string email);
}