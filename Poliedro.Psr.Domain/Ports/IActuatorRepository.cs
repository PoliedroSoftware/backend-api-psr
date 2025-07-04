using Poliedro.Psr.Domain.Entites;

namespace Poliedro.Psr.Domain.Ports;

public interface IActuatorRepository
{
    Task<ActuatorEntity> GetAllActuatorsAsync();
    Task<ActuatorEntity> GetActuatorByIdAsync(string id);
    Task CreateActuatorAsync(ActuatorEntity actuator);
    Task UpdateActuatorAsync(string id, ActuatorEntity actuator);
    Task DeleteActuatorAsync(string id);
}
