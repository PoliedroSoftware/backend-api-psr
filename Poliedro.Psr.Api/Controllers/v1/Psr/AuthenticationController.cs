namespace Poliedro.Psr.Api.Controllers.v1.Psr;
using Microsoft.AspNetCore.Mvc;
using Poliedro.Psr.Application.Dto;
using Poliedro.Psr.Application.Mock;
using Poliedro.Psr.Domain.Wrapper;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthenticationController : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(
        [FromBody] AuthenticationRequest request)
    {
        var response = new ApiResponse<string>
        {

            Data = MockDataFactory.Login(request).ToString(),
            CorrelationId = Guid.NewGuid().ToString(),
        };
        return Ok(await Task.FromResult(response));
    }

    [HttpPost("verifyphone")]
    public async Task<ActionResult<string>> VerifiPhone(
       [FromBody] VerifyPhoneRequest verifyPhone)
    {
        var response = new ApiResponse<bool>
        {
            Data = MockDataFactory.VerifyPhone(verifyPhone),
            CorrelationId = Guid.NewGuid().ToString(),
        };
        return Ok(await Task.FromResult(response));
    }

    [HttpPost("registry")]
    public async Task<ActionResult<ApiResponse<string>>> Register(
     [FromBody] AuthenticationRequest registry)
    {
        var response = new ApiResponse<string>
        {
            Data = MockDataFactory.Registry(registry),
            CorrelationId = Guid.NewGuid().ToString(),
        };
        return Created("registry", response);
    }
}

