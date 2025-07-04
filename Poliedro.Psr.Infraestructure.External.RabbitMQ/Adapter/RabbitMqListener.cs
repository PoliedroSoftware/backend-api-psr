using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poliedro.Psr.Domain.Ports;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Poliedro.Psr.Infraestructure.External.RabbitMQ.Adapter
{
    public class RabbitMqListener : IListenerImagesReceive
    {
        private readonly string _rabbitMqHostName;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IServiceProvider _serviceProvider;
        private const string OpenAIEndpoint = "https://api.openai.com/v1/chat/completions";
        int index = 500;
        public RabbitMqListener(
            IConfiguration configuration,
            IServiceScopeFactory serviceScopeFactory,
            IServiceProvider serviceProvider)
        {
            var rabbitMqSettings = configuration.GetSection("RabbitMqSettings");
            _rabbitMqHostName = rabbitMqSettings["HostName"]!;
            _queueName = rabbitMqSettings["QueueName"]!;
            _username = rabbitMqSettings["UserName"]!;
            _password = rabbitMqSettings["Password"]!;
            _apiKey = configuration["OpenAI:ApiKey"]!;

            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            _serviceScopeFactory = serviceScopeFactory;
            _serviceProvider = serviceProvider;
        }

        public void StartListening()
        {
            if (!string.IsNullOrEmpty(_rabbitMqHostName))
            {
                var factory = new ConnectionFactory()
                {
                    HostName = _rabbitMqHostName,
                    UserName = _username,
                    Password = _password
                };

                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    
                    
                    try
                    {

                        index += 1;
                        string fileName = $"{index}.jpg";
                        string fullPath = Path.Combine(@"C:\imagesRabbitMQ", fileName);
                        File.WriteAllBytes(fullPath, body);

                        string labelsPath = Path.Combine(@"C:\imagesRabbitMQ", "labels.txt");
                        using (StreamWriter writer = new StreamWriter(labelsPath, append: true))
                        {
                            writer.WriteLine(fileName);
                        }





                        //string response = await SendImageAndPromptAsync(imageBytes);
                        //using (var scope = _serviceProvider.CreateScope())
                        //{
                        //    var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
                        //    await notificationService.NotifyAsync(response);
                        //}
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                };

                channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);

                Console.WriteLine("Esperando mensajes de RabbitMQ...");
                //Console.ReadLine();
            }
        }

        public async Task<string> SendImageAndPromptAsync(byte[] imageBytes)
        {
            string base64Image = Convert.ToBase64String(imageBytes);
            var payload = new
            {
                model = "ft:gpt-4o-2024-08-06:personal:psr:Aum0b5g4",
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = $"This is an image in base64: {base64Image}. What number is shown in it?"
                    }
               }
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Respuesta de OpenAI: {responseContent}");
                return responseContent;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error al enviar la imagen: {errorContent}");
                throw new Exception("Error en la solicitud a OpenAI");
            }
        }

    }
}
