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
            await Task.Run(() => _listenerImagesReceive.StartListening(), stoppingToken);
            await Task.Delay(15000, stoppingToken);
        }
    }
}

