namespace Clean_Architecture_Template_Tests.PasswordHasherTests;

public class PasswordHasherTests
{
    private readonly IPasswordHasher _passwordHasher = new PasswordHasher();

    [Fact]
    public void GenerateHash_ShouldReturn_HashWithSalt()
    {
        // Arrange
        string password = "TestPassword123";

        // Act
        string result = _passwordHasher.GenerateHash(password);

        // Assert
        result.Should().NotBeNullOrEmpty();
        result.Split('-').Length
            .Should()
            .Be(2, "because the hash and salt are separated by a '-'");
    }

    [Fact]
    public void VerifyHash_ShouldReturn_TrueForCorrectPassword()
    {
        // Arrange
        string password = "TestPassword123";
        string passwordHash = _passwordHasher.GenerateHash(password);

        // Act
        bool result = _passwordHasher.VerifyHash(password, passwordHash);

        // Assert
        result.Should()
            .BeTrue("because the correct password should match the generated hash");
    }

    [Fact]
    public void VerifyHash_ShouldReturn_FalseForIncorrectPassword()
    {
        // Arrange
        string password = "TestPassword123";
        string wrongPassword = "WrongPassword123";
        string passwordHash = _passwordHasher.GenerateHash(password);

        // Act
        bool result = _passwordHasher.VerifyHash(wrongPassword, passwordHash);

        // Assert
        result.Should()
            .BeFalse("because the incorrect password should not match the generated hash");
    }

    [Fact]
    public void GenerateHash_ShouldProduceDifferentHashesForSamePassword()
    {
        // Arrange
        string password = "TestPassword123";

        // Act
        string hash1 = _passwordHasher.GenerateHash(password);
        string hash2 = _passwordHasher.GenerateHash(password);

        // Assert
        hash1.Should()
            .NotBe(hash2, "because generating a hash for the same password multiple times should produce different hashes due to the salt");
    }
}