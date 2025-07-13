
namespace AiRagProxy.Api.Services;

public class EncryptionService : IEncryptionService
{
    private readonly IStringDataProtector _protector;

    public EncryptionService(IStringDataProtector protector)
    {
        _protector = protector;
    }

    public string Encrypt(string plainText)
    {
        return _protector.Protect(plainText);
    }

    public string Decrypt(string cipherText)
    {
        return _protector.Unprotect(cipherText);
    }
}
