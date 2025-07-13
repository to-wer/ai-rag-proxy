namespace AiRagProxy.Api.Services;

public interface IStringDataProtector
{
    string Protect(string plainText);
    string Unprotect(string cipherText);
}
