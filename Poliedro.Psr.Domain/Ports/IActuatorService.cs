using Poliedro.Psr.Domain.Entites;

namespace Poliedro.Psr.Domain.Ports;

public interface IActuatorService
{
    Task SendMessagesAsync(ActuatorEntity actuatorEntity);
}
