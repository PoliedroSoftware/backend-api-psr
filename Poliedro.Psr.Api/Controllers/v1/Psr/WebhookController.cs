using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

namespace Poliedro.Psr.Api.Controllers.v1.Psr
{
    [Route("[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        private readonly string _graphApiToken;
        private readonly string _webhookVerifyToken;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        public WebhookController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiKey = configuration["OpenAI:ApiKey"]!;
            _graphApiToken = configuration["OpenAI:GraphApiTokken"]!;
            _webhookVerifyToken = configuration["OpenAI:WebhookVerifyToken"]!;
        }

        [HttpPost]
        public async Task<IActionResult> ReceiveMessage([FromBody] JsonElement body)
        {
            // Log incoming message
            Console.WriteLine("Incoming webhook message: " + JsonSerializer.Serialize(body, new JsonSerializerOptions { WriteIndented = true }));

            // Parse the incoming message
            if (body.TryGetProperty("entry", out var entry) && entry[0].TryGetProperty("changes", out var changes))
            {
                var message = changes[0].GetProperty("value").GetProperty("messages")[0];
                var messageType = message.GetProperty("type").GetString();

                // If the incoming message contains text
                if (messageType == "text")
                {
                    // Extract the business phone number ID
                    var businessPhoneNumberId = changes[0].GetProperty("value").GetProperty("metadata").GetProperty("phone_number_id").GetString();
                    var from = message.GetProperty("from").GetString();
                    var textBody = message.GetProperty("text").GetProperty("body").GetString();

                    // Prepare response payload
                    var responseMessage = new
                    {
                        messaging_product = "whatsapp",
                        to = from,
                        text = new { body = "Echo: " + textBody },
                        context = new { message_id = message.GetProperty("id").GetString() }
                    };

                    // Send reply message
                    var content = new StringContent(JsonSerializer.Serialize(responseMessage), Encoding.UTF8, "application/json");
                    var url = $"https://graph.facebook.com/v18.0/{businessPhoneNumberId}/messages";
                    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _graphApiToken);

                    var response = await _httpClient.PostAsync(url, content);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Message sent successfully.");
                        return Ok();
                    }
                    else
                    {
                        Console.WriteLine("Failed to send message: " + response.ReasonPhrase);
                        return StatusCode((int)response.StatusCode);
                    }
                }
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult VerifyWebhook(
        [FromQuery(Name = "hub.mode")] string? hub_mode,
        [FromQuery(Name = "hub.verify_token")] string? hub_verify_token,
        [FromQuery(Name = "hub.challenge")] string? hub_challenge)
        {
            if (hub_mode == "subscribe" && hub_verify_token == _webhookVerifyToken)
            {
                Console.WriteLine("Webhook verified successfully!");
                return Ok(hub_challenge);
            }
            else
            {
                return StatusCode(403);
            }
        }

    }
}
