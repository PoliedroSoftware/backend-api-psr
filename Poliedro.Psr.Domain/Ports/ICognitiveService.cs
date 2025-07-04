using Poliedro.Psr.Domain.Entites;

namespace Poliedro.Psr.Domain.Ports;

public interface ICognitiveService
{
    Task<UserReaderEntity> ProcessImageAsync(Stream imageStream);
}

