using Microsoft.Extensions.DependencyInjection;
using Poliedro.Psr.Domain.Ports;
using Poliedro.Psr.Infraestructure.External.Azure.Adapter.CongnitiveServices;
using Poliedro.Psr.Infraestructure.External.RabbitMQ.Adapter;
namespace Poliedro.Psr.Infraestructure.External.RabbitMQ;

public static class DependencyInjectionService
{
    public static IServiceCollection AddRabbitMQ(
        this IServiceCollection services)
    {
        services.AddSingleton<IListenerImagesReceive, RabbitMqListener>();
        services.AddTransient<ICognitiveService, CongnitiveServices>();
        return services;
    }
}

