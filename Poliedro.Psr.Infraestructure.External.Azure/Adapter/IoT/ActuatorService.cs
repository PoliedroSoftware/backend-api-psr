using System.Text;
using System.Text.Json;
using Poliedro.Psr.Domain.Entites;
using Poliedro.Psr.Domain.Ports;

namespace Poliedro.Psr.Infraestructure.External.Azure.Adapter.IoT;

public class ActuatorService : IActuatorService
{
    public async Task SendMessagesAsync(ActuatorEntity actuatorEntity)
    {
        string apiUrl = "";
        using HttpClient client = new();
        string jsonContent = JsonSerializer.Serialize(actuatorEntity);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        try
        {
            HttpResponseMessage response = await client.PostAsync(apiUrl, content);
            var responseData = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Solicitud enviada con éxito. Estado: " + response.StatusCode);
                Console.WriteLine("Respuesta de la API: " + responseData);
            }
            else
            {
                Console.WriteLine("Error al enviar la solicitud. Estado: " + response.StatusCode);
                Console.WriteLine("Contenido de la respuesta: " + responseData);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al enviar la solicitud: " + ex.Message);
        }
    }
}

