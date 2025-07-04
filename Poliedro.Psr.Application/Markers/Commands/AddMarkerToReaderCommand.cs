using MediatR;
using Poliedro.Psr.Application.Dto;
using Poliedro.Psr.Domain.Entites;

namespace Poliedro.Psr.Application.Markers.Commands
{
    public record AddMarkerToReaderCommand(
        Guid ReaderGuid, 
        MarkerEntity Marker
    ) : IRequest<bool>;

}
