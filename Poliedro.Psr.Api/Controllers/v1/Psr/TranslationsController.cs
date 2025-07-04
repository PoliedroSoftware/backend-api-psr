using MediatR;
using Microsoft.AspNetCore.Mvc;
using Poliedro.Psr.Application.Dto;
using Poliedro.Psr.Application.Translations.Query;
using Poliedro.Psr.Domain.Wrapper;

namespace Poliedro.Psr.Api.Controllers.v1.Psr;

[ApiController]
[Route("api/v1/[controller]")]
public class TranslationsController(IMediator _mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<TranslationsAvailableDto>> GetAllTranslationsAsync()
    {
        var response = await _mediator.Send(new GetTranslationsQuery());
        var responseOK = new ApiResponse<TranslationsAvailableDto>
        {
            Data = response,
            CorrelationId = Guid.NewGuid().ToString(),
        };
        return Ok(responseOK);
    }
}
