using Poliedro.Psr.Domain.Ports;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Poliedro.Psr.Infraestructure.External.Azure.Adapter.Twilio;

public class SendMessage : ISendMessages
{
     public async Task SendAsync(string messages, string number)
    {
        var accountSid = "";
        var authToken = "";
        TwilioClient.Init(accountSid, authToken);

        var messageOptions = new CreateMessageOptions(
          new PhoneNumber($"whatsapp:{number}"))
        {
            From = new PhoneNumber("whatsapp:+14155238886"),
            Body = messages
        };

        var message = await Task.FromResult( MessageResource.Create(messageOptions));
        Console.WriteLine(message.Body);
    }
}
