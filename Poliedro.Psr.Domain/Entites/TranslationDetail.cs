using System.Text.Json.Serialization;

namespace Poliedro.Psr.Domain.Entites;

public class TranslationDetail
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
}
