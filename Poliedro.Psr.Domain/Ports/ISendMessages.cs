namespace Poliedro.Psr.Domain.Ports;

public interface ISendMessages
{
    Task SendAsync(string messages, string number);
}
