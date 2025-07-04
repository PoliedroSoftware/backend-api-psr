using Poliedro.Psr.Domain.Ports;

namespace WorkerServiceIoT.RabbitMQ;

public class ListenerImageReceiveWorker(
    ILogger<ListenerImageReceiveWorker> _logger,
    IListenerImagesReceive _listenerImagesReceive
   ) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation($"Starting Reading RabbitMq Service...");
            _listenerImagesReceive.StartListening();
            await Task.Delay(60000, stoppingToken);
        }
    }
}

