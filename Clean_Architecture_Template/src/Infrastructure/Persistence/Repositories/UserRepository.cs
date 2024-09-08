namespace Infrastructure.Persistence.Repositories;

public class UserRepository(CleanArchitectureTemplateDbContext passwordHasherAppDbContext) : IUserRepository
{
    public async Task<bool> CreateUser(User user)
    {
        passwordHasherAppDbContext.Add(user);

        try
        {
            await passwordHasherAppDbContext.SaveChangesAsync();
            return true;


        }
        //Unique Exception Catch
        catch (DbUpdateException ex)
            when (ex.InnerException is SqlException { Number: 2627 })
        {
            Log.Error(ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            throw;

        }

    }

    public async Task<Result<User>> Login(User user)
    {
        var dbUser = await GetUserByEmail(user.Email);

        if (!dbUser.Success)
        {
            return Result<User>.CreateFailure("User Not Found");
        }

        return Result<User>.CreateSuccess(user);
    }

    public async Task<Result<User>> GetUserByEmail(string email)
    {
        var user = await passwordHasherAppDbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));

        if (user is null)
        {
            return Result<User>.CreateFailure("User Not Found");
        }

        // Return the found user
        return Result<User>.CreateSuccess(user);
    }
}