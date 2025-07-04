namespace Poliedro.Psr.Infraestructure.External.Azure.Adapter.IoT;

using Microsoft.Azure.Devices;
using Microsoft.Extensions.Options;
using Poliedro.Psr.Domain.IoT;
using Poliedro.Psr.Domain.Ports;
using System.Text;
using System.Threading.Tasks;

public class IoTService(IOptions<IoTHubSettings> iotHubSettings) : IIoTService
{
    private readonly ServiceClient _serviceClient = ServiceClient.CreateFromConnectionString(iotHubSettings.Value.Hub);
    private readonly string _targetDeviceId = iotHubSettings.Value.DeviceId;

    public async Task SendMessageAsync(string message)
    {
        var messageBytes = Encoding.UTF8.GetBytes(message);
        var iotMessage = new Microsoft.Azure.Devices.Message(messageBytes);
        await _serviceClient.SendAsync(_targetDeviceId, iotMessage);
    }

   
    public async Task InvokeDeviceMethodAsync(string methodName, string payload)
    {
        var methodInvocation = new CloudToDeviceMethod(methodName) { ResponseTimeout = TimeSpan.FromSeconds(30) };
        methodInvocation.SetPayloadJson(payload);

        var response = await _serviceClient.InvokeDeviceMethodAsync(_targetDeviceId, methodInvocation);
        Console.WriteLine($"Invocado el método {methodName} en el dispositivo {_targetDeviceId} con respuesta: {response.GetPayloadAsJson()}");
    }

 
    public async Task CloseAsync()
    {
        await _serviceClient.CloseAsync();
    }

   
}
