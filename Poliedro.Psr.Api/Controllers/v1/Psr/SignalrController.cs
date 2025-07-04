using Microsoft.AspNetCore.Mvc;
using Poliedro.Psr.Domain.Ports;

namespace Poliedro.Psr.Api.Controllers.v1.Psr
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SignalrController(INotificationService _notificationService) : ControllerBase
    {
        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] string request, string num)
        {
            await _notificationService.NotifyAsync(request);
            return Ok(new { Status = "Message sent" });
        }
    }
}
