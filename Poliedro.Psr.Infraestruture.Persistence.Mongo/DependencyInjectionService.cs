using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Poliedro.Psr.Domain.Ports;
using Poliedro.Psr.Infraestruture.Persistence.Mongo.Adapter.Actuator;
using Poliedro.Psr.Infraestruture.Persistence.Mongo.Adapter.LastReader;
using Poliedro.Psr.Infraestruture.Persistence.Mongo.Adapter.Markers;
using Poliedro.Psr.Infraestruture.Persistence.Mongo.Adapter.Reader;
using Poliedro.Psr.Infraestruture.Persistence.Mongo.Adapter.Translations;

namespace Poliedro.Psr.Infraestructure.Persistence.Mongo;

public static class DependencyInjectionService
{
    public static IServiceCollection AddPersistenceMongo(
        this IServiceCollection services,
        IConfiguration configuration)
    {
       var mongoConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION") ??  configuration.GetConnectionString("MongoDb");
        services.AddSingleton<IMongoClient>(new MongoClient(mongoConnectionString));
        services.AddScoped<IMongoDatabase>(sp => sp.GetRequiredService<IMongoClient>().GetDatabase("psr"));
        services.AddScoped<IReaderRepository, ReaderRepository>();
        services.AddSingleton<ITranslationService, TranslationsService>();
        services.AddScoped<IActuatorRepository, ActuatorRepository>();
        services.AddScoped<ILastReaderRepository, LastReaderRepository>(); 
        services.AddScoped<IRealTimeService, ActuadorAiService>();
        services.AddScoped<IMarkerRepository, MarkerRepository>();
        return services;
   }
}
