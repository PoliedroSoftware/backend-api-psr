using MediatR;
using Poliedro.Psr.Domain.Entites;

namespace Poliedro.Psr.Application.Actuator.Querys;

public class GetAllActuatorsQuery : IRequest<ActuatorEntity>
{
}
