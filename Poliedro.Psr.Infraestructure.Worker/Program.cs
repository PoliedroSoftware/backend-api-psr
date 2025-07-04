using Poliedro.Psr.Domain.Ports;
using Poliedro.Psr.Infraestructure.External.RabbitMQ.Adapter;
using WorkerServiceIoT.RabbitMQ;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ListenerImageReceiveWorker>();
builder.Services.AddSingleton<IListenerImagesReceive, RabbitMqListener>(); 

var host = builder.Build();
host.Run();
