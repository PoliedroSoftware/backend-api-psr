using Microsoft.AspNetCore.Mvc;
using Poliedro.Psr.Domain.Ports;

namespace Poliedro.Psr.Api.Controllers.v1.Psr
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WhatsappController(ISendMessages sendMessages) : ControllerBase
    {
        [HttpPost("send")]
        public async Task<IActionResult> SendNotification([FromBody] string request, string number)
        {
            await sendMessages.SendAsync(request, number);
            return Ok(new { Status = "Message sent" });
        }
    }
   
}
