using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Poliedro.Psr.Application.Actuator.Commands;
using Poliedro.Psr.Domain.Ports;

namespace Poliedro.Psr.Application.Actuator.Handle;

public class UpdateActuatorCommandHandler(
    IActuatorRepository _actuatorRepository, 
    IIoTService _ioTService, IActuatorService _actuatorService) : IRequestHandler<UpdateActuatorCommand, IActionResult>
{
    public async Task<IActionResult> Handle(UpdateActuatorCommand request, CancellationToken cancellationToken)
    {
        var actuator = await _actuatorRepository.GetActuatorByIdAsync(request.Id.ToString());
        if (actuator == null)
        {
            return new NotFoundResult();
        }

        actuator.Valve = request.Valve;
        actuator.SolenoidValve = request.SolenoidValve;
        actuator.Bomb = request.Bomb;
        actuator.Circuit = request.Circuit;
        actuator.Raspberry = request.Raspberry;

        await _actuatorRepository.UpdateActuatorAsync(request.Id.ToString(), actuator);
        await _ioTService.SendMessageAsync(JsonConvert.SerializeObject(actuator));
        return new NoContentResult();
    }
}

