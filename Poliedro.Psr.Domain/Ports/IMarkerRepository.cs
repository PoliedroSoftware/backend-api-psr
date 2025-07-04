using Poliedro.Psr.Domain.Entites;


namespace Poliedro.Psr.Domain.Ports
{
    public interface IMarkerRepository
    {
        Task<bool> AddMarkerToReaderAsync(Guid readerGuid, MarkerEntity marker, CancellationToken cancellationToken);

    }
}
