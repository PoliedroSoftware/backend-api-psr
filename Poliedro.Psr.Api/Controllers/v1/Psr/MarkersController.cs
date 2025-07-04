using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poliedro.Psr.Application.Markers.Commands;

namespace Poliedro.Psr.Api.Controllers.v1.Psr
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkersController(IMediator _mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> AddMarkerToReader(
           [FromBody] AddMarkerToReaderCommand command,
           [FromServices] IValidator<AddMarkerToReaderCommand> validator)
        {
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
