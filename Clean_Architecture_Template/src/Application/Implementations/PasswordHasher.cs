using Domain.Abstraction;

namespace Application.Implementations;

public sealed class PasswordHasher : IPasswordHasher
{
    //16 bytes == 128 bits recomendend size
    private const int SaltSize = 16;

    //32 bytes == 256 bits recomendend size
    private const int HashSize = 32;

    //how many itterations
    private const int Itterations = 100000;

    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;
    public string GenerateHash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Itterations, Algorithm, HashSize);

        return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
    }

    public bool VerifyHash(string password, string passwordHash)
    {
        string[] parts = passwordHash.Split('-');
        byte[] hash = Convert.FromHexString(parts[0]);
        byte[] salt = Convert.FromHexString(parts[1]);

        byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password,
            salt,
            Itterations,
            Algorithm,
            HashSize);

        return CryptographicOperations.FixedTimeEquals(hash, inputHash);

    }
}