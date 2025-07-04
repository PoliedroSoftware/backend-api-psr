using Poliedro.Psr.Domain.Entites;

namespace Poliedro.Psr.Domain.Ports;

public interface IReaderRepository
{
    Task<List<UserEntity>> ExecuteAsync();
}
