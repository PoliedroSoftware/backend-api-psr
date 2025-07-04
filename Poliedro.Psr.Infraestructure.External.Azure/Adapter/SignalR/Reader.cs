using Microsoft.AspNetCore.SignalR;

namespace Poliedro.Psr.Infraestructure.External.Azure.Adapter.SignalR;

public class Reader : Hub
{
    public async Task SendMessage(string data)
    {
        await Clients.All.SendAsync("ReceiveMessage", data);
    }
}
