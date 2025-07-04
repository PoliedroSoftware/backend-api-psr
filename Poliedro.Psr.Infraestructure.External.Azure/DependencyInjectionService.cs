using Microsoft.Extensions.DependencyInjection;
using Poliedro.Psr.Domain.Ports;
using Poliedro.Psr.Infraestructure.External.Azure.Adapter.IoT;
using Poliedro.Psr.Infraestructure.External.Azure.Adapter.SignalR;
using Poliedro.Psr.Infraestructure.External.Azure.Adapter.Twilio;

namespace Poliedro.Psr.Infraestructure.External.Azure;

public static class DependencyInjectionService
{
    public static IServiceCollection AddAzureIoTHub(
        this IServiceCollection services)
    {
        services.AddTransient<IIoTService, IoTService>();
        services.AddTransient<IActuatorService, ActuatorService>();
        services.AddTransient<ISendMessages, SendMessage>();
        services.AddScoped<INotificationService, SignalRNotificationService>();
        
        return services;
    }
}

