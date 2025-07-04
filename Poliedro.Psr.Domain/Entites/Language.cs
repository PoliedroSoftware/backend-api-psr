using System.Text.Json.Serialization;

namespace Poliedro.Psr.Domain.Entites;

public class Language
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("tag")]
    public string Tag { get; set; }
    [JsonPropertyName("originalName")]
    public string OriginalName { get; set; }
    [JsonPropertyName("flagEmoji")]
    public string FlagEmoji { get; set; }
    public string Code { get; set; }
}
