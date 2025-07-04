using Poliedro.Psr.Domain.Entites.Pinecone;
using Poliedro.Psr.Domain.Ports;
using System.Text.Json;
using System.Text;

namespace Pinecone.Adapter;

public class PineconeService(HttpClient _httpClient) : IPinceconeService
{
    public async Task<OpenAiPineconeResponseEntity> Execute(string prompt)
    {
        var url = "";
        var requestContent = new StringContent(
            JsonSerializer.Serialize(new { prompt }),
            Encoding.UTF8,
            "application/json");
        var response = await _httpClient.PostAsync(url, requestContent);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OpenAiPineconeResponseEntity>(responseContent)!;
        }
        return new OpenAiPineconeResponseEntity(new TokenUsageEntity(0, 0,0), new CostEntity(0, 0, 0), "Error");
    }
}
