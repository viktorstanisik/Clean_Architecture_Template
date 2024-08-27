namespace Clean_Architecture_Template_Infrastructure_Layer.Repositories;

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

    public async Task<User> Login(User user)
    {
        var dbUser = await GetUser(user.Email);

        if (dbUser == null)
        {
            throw new Exception("User Not Found");
        }

        return dbUser;
    }

    public async Task<User> GetUser(string email)
    {
        return await passwordHasherAppDbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));

    }
}