using Poliedro.Psr.Domain.IoT;
namespace Poliedro.Psr.Domain.Ports;
public interface IIoTService
{
    Task SendMessageAsync(string message);
}

