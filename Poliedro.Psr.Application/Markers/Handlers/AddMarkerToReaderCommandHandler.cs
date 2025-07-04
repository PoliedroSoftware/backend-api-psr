using AutoMapper;
using MediatR;
using Poliedro.Psr.Application.Markers.Commands;
using Poliedro.Psr.Domain.Ports;

namespace Poliedro.Psr.Application.Markers.Handlers
{
    internal class AddMarkerToReaderCommandHandler(IMarkerRepository _markerRepository, IMapper _mapper) : IRequestHandler<AddMarkerToReaderCommand, bool>
    {
        public async Task<bool> Handle(AddMarkerToReaderCommand request, CancellationToken cancellationToken)
        {
            var (readerGuid, marker) = request;

            var success = await _markerRepository.AddMarkerToReaderAsync(readerGuid, marker, cancellationToken);

            return success;
        }
    }
}
