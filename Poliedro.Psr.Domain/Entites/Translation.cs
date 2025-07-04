using System.Text.Json.Serialization;

namespace Poliedro.Psr.Domain.Entites;

public class Translation
{
    [JsonPropertyName("keyName")]
    public string KeyName { get; set; }
    [JsonPropertyName("translations")]
    public Dictionary<string, TranslationDetail> Translations { get; set; }
}
