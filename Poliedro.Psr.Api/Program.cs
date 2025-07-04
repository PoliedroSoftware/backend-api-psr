using Poliedro.Psr.Api;
using Poliedro.Psr.Application;
using Poliedro.Psr.Domain.IoT;
using Poliedro.Psr.Infraestructure.External.Azure;
using Poliedro.Psr.Infraestructure.External.Azure.Adapter.SignalR;
using Poliedro.Psr.Infraestructure.External.RabbitMQ;
using Poliedro.Psr.Infraestructure.Persistence.Mongo;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using StackExchange.Redis;
using WorkerServiceIoT.RabbitMQ;
using Pinecone;
var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithExceptionDetails()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(config["Elasticsearch:Uri"]!))
    {
        AutoRegisterTemplate = true,
        IndexFormat = "logs-{0:yyyy.MM}"
    })
    .CreateLogger();

try
{
    Log.Information("Starting application");
    builder.Services.AddSignalR();
    builder.Host.UseSerilog();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("PoliedroPsr", policy =>
        {
            var allowedOrigins = config.GetSection("AllowedOrigins").Get<List<string>>();
            policy.WithOrigins(allowedOrigins.ToArray())
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
    });

    builder.Services
        .AddWebApi()
        .AddApplication()
        .AddPersistenceMongo(config)
        .AddRabbitMQ()
        .AddAzureIoTHub()
        .AddOpenAiPinecone();

    builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    {
        var configuration = ConfigurationOptions.Parse(config["Redis:ConnectionString"]!, true);
        return ConnectionMultiplexer.Connect(configuration);
    });

    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = config["Redis:ConnectionString"];
        options.InstanceName = "ReadersApi_";
    });

    builder.Services.AddScoped<IDatabase>(sp =>
    {
        var connection = sp.GetRequiredService<IConnectionMultiplexer>();
        return connection.GetDatabase();
    });

    builder.Services.AddControllers();
    builder.Services.AddRouting(routing => routing.LowercaseUrls = true);
    builder.Services.AddSwaggerGen();

    builder.Services.Configure<IoTHubSettings>(config.GetSection("IoTHub"));

    builder.Services.AddHttpClient();
    builder.Services.AddHostedService<ListenerImageReceiveWorker>();

    var app = builder.Build();
    app.UseCors("PoliedroPsr");
    app.UseRouting();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });

    app.MapControllers();
    app.MapHub<Reader>("/reader");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
