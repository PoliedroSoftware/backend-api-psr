using MediatR;
using Poliedro.Psr.Domain.Entites;
using Poliedro.Psr.Domain.Ports;

namespace Poliedro.Psr.Application.Actuator.Querys;

public class GetAllActuatorsQueryHandler(IActuatorRepository _actuatorRepository) : IRequestHandler<GetAllActuatorsQuery, ActuatorEntity>
{
    public async Task<ActuatorEntity> Handle(GetAllActuatorsQuery request, CancellationToken cancellationToken)
    {
        return await _actuatorRepository.GetAllActuatorsAsync();
    }
}
