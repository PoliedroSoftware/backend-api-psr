using Poliedro.Psr.Domain.Entites;

namespace Poliedro.Psr.Domain.Ports;

public interface ILastReaderRepository
{
    Task CreateActuatorAsync(LastReaderEntity lastReaderEntity);
}
