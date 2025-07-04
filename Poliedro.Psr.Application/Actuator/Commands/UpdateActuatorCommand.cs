using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Poliedro.Psr.Application.Actuator.Commands;

public class UpdateActuatorCommand : IRequest<IActionResult>
{
    public string Id { get; set; }
    public bool Valve { get; set; }
    public bool SolenoidValve { get; set; }
    public bool Bomb { get; set; }
    public bool Circuit { get; set; }
    public bool Raspberry { get; set; }
}
