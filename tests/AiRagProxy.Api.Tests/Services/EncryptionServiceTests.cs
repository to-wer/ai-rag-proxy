using AiRagProxy.Api.Services;
using Moq;
using Xunit;

namespace AiRagProxy.Api.Tests.Services;

public class EncryptionServiceTests
{
    private readonly EncryptionService _encryptionService;
    private readonly Mock<IStringDataProtector> _mockStringDataProtector;

    public EncryptionServiceTests()
    {
        _mockStringDataProtector = new Mock<IStringDataProtector>();
        _encryptionService = new EncryptionService(_mockStringDataProtector.Object);
    }

    [Fact]
    public void Encrypt_ShouldReturnEncryptedString()
    {
        // Arrange
        var plainText = "test_plain_text";
        var cipherText = "encrypted_test_text";
        _mockStringDataProtector.Setup(p => p.Protect(plainText)).Returns(cipherText);

        // Act
        var result = _encryptionService.Encrypt(plainText);

        // Assert
        Assert.Equal(cipherText, result);
    }

    [Fact]
    public void Decrypt_ShouldReturnDecryptedString()
    {
        // Arrange
        var plainText = "test_plain_text";
        var cipherText = "encrypted_test_text";
        _mockStringDataProtector.Setup(p => p.Unprotect(cipherText)).Returns(plainText);

        // Act
        var result = _encryptionService.Decrypt(cipherText);

        // Assert
        Assert.Equal(plainText, result);
    }

    [Fact]
    public void Decrypt_ShouldThrowException_WhenCipherTextIsInvalid()
    {
        // Arrange
        var invalidCipherText = "invalid_cipher_text";
        _mockStringDataProtector.Setup(p => p.Unprotect(invalidCipherText))
            .Throws<System.Security.Cryptography.CryptographicException>();

        // Act & Assert
        Assert.Throws<System.Security.Cryptography.CryptographicException>(() => 
            _encryptionService.Decrypt(invalidCipherText));
    }
}
