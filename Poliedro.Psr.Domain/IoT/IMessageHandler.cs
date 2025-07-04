namespace Poliedro.Psr.Domain.IoT;

public interface IMessageHandler
{
    void HandleMessage(string message);
}

