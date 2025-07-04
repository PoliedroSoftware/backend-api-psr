using Microsoft.Extensions.DependencyInjection;
using Pinecone.Adapter;
using Poliedro.Psr.Domain.Ports;
namespace Pinecone;

public static class DependencyInjectionService
{
    public static IServiceCollection AddOpenAiPinecone(
        this IServiceCollection services)
    {
        services.AddTransient<IPinceconeService, PineconeService>();
        return services;
    }
}

