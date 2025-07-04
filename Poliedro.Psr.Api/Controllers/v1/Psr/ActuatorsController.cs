using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poliedro.Psr.Application.Actuator.Commands;
using Poliedro.Psr.Application.Actuator.Querys;
using Poliedro.Psr.Domain.Entites;
using Poliedro.Psr.Domain.Wrapper;
namespace Poliedro.Psr.Api.Controllers.v1.Psr;

[Route("api/v1/[controller]")]
[ApiController]
public class ActuatorsController(IMediator _mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ActuatorEntity>> GetAllActuators()
    {
        var query = new GetAllActuatorsQuery();
        var actuators = await _mediator.Send(query);
        var responseOK = new ApiResponse<ActuatorEntity>
        {
            Data = actuators,
            CorrelationId = Guid.NewGuid().ToString(),
        };
        return Ok(responseOK);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateActuator([FromRoute] string id, [FromBody] UpdateActuatorCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID in route and body do not match.");
        }

        return await _mediator.Send(command);
    }
}
