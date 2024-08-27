namespace Clean_Architecture_Template_Domain_Layer.Abstraction;

public interface IPasswordHasher
{
    public string GenerateHash(string password);
    public bool VerifyHash(string password, string passwordHash);
}