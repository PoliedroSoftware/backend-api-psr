using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poliedro.Psr.Domain.OpenAi;
using System.Text;
using System.Text.Json.Serialization;
[Route("api/[controller]")]
[ApiController]
public class OpenAIController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private static string _contenidoPDF;
    private readonly IMediator _mediator;


    public OpenAIController(IHttpClientFactory httpClientFactory, IConfiguration configuration, IMediator mediator)
    {
        _httpClient = httpClientFactory.CreateClient();
        _apiKey = configuration["OpenAI:ApiKey"];
        _mediator = mediator;
    }


    [HttpPost("generate")]
    public async Task<IActionResult> GenerateResponse([FromBody] OpenAIRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var apiUrl = "http://68.183.144.244:11434/v1/chat/completions";

            var requestBody = new
            {
                model = "deepseek-r1:1.5b",
                messages = new[] { new { role = "user", content = request.Prompt } },
                temperature = 0.7
            };

            var jsonRequest = System.Text.Json.JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var chatResponse = System.Text.Json.JsonSerializer.Deserialize<ChatResponse>(responseBody);

            return Ok(chatResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    // TODO: mover
    public class ChatResponse
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("object")]
        public string Object { get; set; }

        [JsonPropertyName("created")]
        public long Created { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("system_fingerprint")]
        public string SystemFingerprint { get; set; }

        [JsonPropertyName("choices")]
        public Choice[] Choices { get; set; }

        [JsonPropertyName("usage")]
        public Usage Usage { get; set; }
    }

    public class Choice
    {
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("message")]
        public Message Message { get; set; }

        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
    }

    public class Message
    {
        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }

    public class Usage
    {
        [JsonPropertyName("prompt_tokens")]
        public int PromptTokens { get; set; }

        [JsonPropertyName("completion_tokens")]
        public int CompletionTokens { get; set; }

        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }
    }

}
