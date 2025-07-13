using Microsoft.AspNetCore.DataProtection;

namespace AiRagProxy.Api.Services;

public class StringDataProtector : IStringDataProtector
{
    private readonly IDataProtector _protector;

    public StringDataProtector(IDataProtectionProvider provider)
    {
        _protector = provider.CreateProtector(nameof(EncryptionService));
    }

    public string Protect(string plainText)
    {
        return _protector.Protect(plainText);
    }

    public string Unprotect(string cipherText)
    {
        return _protector.Unprotect(cipherText);
    }
}
