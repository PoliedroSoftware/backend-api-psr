namespace Poliedro.Psr.Domain.IoT;
public record IoTMessage(
    string Data,
    Dictionary<string, string> Properties)
{
    public override string ToString()
    {
        var properties = string.Join(", ", Properties.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
        return $"Data: {Data}, Properties: [{properties}]";
    }
};
