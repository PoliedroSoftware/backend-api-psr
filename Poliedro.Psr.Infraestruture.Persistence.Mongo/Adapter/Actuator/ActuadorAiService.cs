using Newtonsoft.Json;
using Poliedro.Psr.Domain.Ports;
using System.Net.WebSockets;
using System.Text;

namespace Poliedro.Psr.Infraestruture.Persistence.Mongo.Adapter.Actuator;

public class ActuadorAiService : IRealTimeService
{
    private const string ApiKey = "";
    private const string Url = "";

    public async Task StartRealtimeSessionAsync()
    {
        using (var ws = new ClientWebSocket())
        {
            // Configura las cabeceras requeridas
            ws.Options.SetRequestHeader("Authorization", $"Bearer {ApiKey}");
            ws.Options.SetRequestHeader("OpenAI-Beta", "realtime=v1");

            // Conectar al WebSocket de OpenAI
            await ws.ConnectAsync(new Uri(Url), CancellationToken.None);
            Console.WriteLine("Conectado al servidor.");

            // Enviar un mensaje inicial para activar la IA en tiempo real
            await SendMessage(ws, new
            {
                type = "response.create",
                response = new
                {
                    modalities = new[] { "text" }, // Especifica que solo esperas texto
                    instructions = "Por favor, responde solo en texto. No incluyas imágenes u otro tipo de contenido multimedia."
                }
            });

            // Recibir y procesar mensajes en tiempo real
            await ReceiveMessages(ws);
        }
    }

    private static async Task SendMessage(ClientWebSocket ws, object message)
    {
        var messageJson = JsonConvert.SerializeObject(message);
        var messageBytes = Encoding.UTF8.GetBytes(messageJson);
        await ws.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);
        Console.WriteLine("Mensaje enviado al servidor.");
    }

    private async Task ReceiveMessages(ClientWebSocket ws)
    {
        var buffer = new byte[2048];

        while (ws.State == WebSocketState.Open)
        {
            var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine($"Mensaje recibido: {message}");

            // Mensaje de prueba para confirmar la conexión
            if (message.Contains("Hola", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Respuesta recibida correctamente.");
            }

            // Procesar otras acciones como apagar válvula
            if (message.Contains("apagar válvula", StringComparison.OrdinalIgnoreCase))
            {
                await ApagarValvula();
            }
        }
        
    }

    private async Task ApagarValvula()
    {
        
      Console.WriteLine("La válvula se ha apagado correctamente.");
        
    }
}
