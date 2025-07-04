namespace Poliedro.Psr.Domain.Settings;

public class RabbitMqSettings
{
    public required string HostName { get; set; }
    public required string QueueName { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
}

