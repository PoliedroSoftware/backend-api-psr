namespace Poliedro.Psr.Domain.OpenAi;

public class OpenAIResponse
{
    public string Id { get; set; }
    public string Object { get; set; }
    public OpenAIChoice[] Choices { get; set; }
}
