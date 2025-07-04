using Microsoft.AspNetCore.SignalR;
using Poliedro.Psr.Domain.Ports;

namespace Poliedro.Psr.Infraestructure.External.Azure.Adapter.SignalR;

public class SignalRNotificationService(IHubContext<Reader> hubContext) : INotificationService
{
    public async Task NotifyAsync(string data)
    {
        await hubContext.Clients.All.SendAsync("ReceiveMessage", data);
    }
}
