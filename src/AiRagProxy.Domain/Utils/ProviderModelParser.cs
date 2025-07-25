namespace AiRagProxy.Domain.Utils;

public static class ProviderModelParser
{
    public static (string providerName, string modelName) ParseProviderAndModel(string model)
    {
        var idx = model.IndexOf('-');
        if (idx < 0)
            throw new ArgumentException("Model name must be prefixed with provider (e.g. openai-gpt-4)");

        var providerName = model[..idx];
        var modelName = model[(idx + 1)..];
        return (providerName, modelName);
    }
}