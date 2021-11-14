public class SecretsConfiguration
{
    public string Secret { get; set; }

    public SecretsConfiguration(string secret) => Secret = secret;
}