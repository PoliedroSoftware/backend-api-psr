//using Microsoft.AspNetCore.Mvc;
//using Poliedro.Psr.Domain.Ports;

//namespace Poliedro.Psr.Api.Controllers.v1.Psr;

//[Route("api/v1/[controller]")]
//[ApiController]
//public class IotController(
//    IIoTService _iotService) : ControllerBase
//{
//    [HttpPost("send")]
//    public async Task<IActionResult> SendMessage([FromBody] string message)
//    {
//        await _iotService.SendMessageAsync(message);
//        return Ok("Message sent");
//    }

//    [HttpGet("receive")]
//    public async Task<IActionResult> ReceiveMessage()
//    {
//        var message = await _iotService.ReceiveCommandFromIoTHubAsync();
//        if (message == null) return NoContent();
//        return Ok(message);
//    }
//}
