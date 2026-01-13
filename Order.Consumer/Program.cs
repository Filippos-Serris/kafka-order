using MediatR;
using Order.Consumer.Configuration;
using Order.Consumer.Infrastructure.Kafka;
using System.Reflection;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<ConsumerKafkaOptions>(
    builder.Configuration.GetSection("Kafka")
);
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});
builder.Services.AddHostedService<OrderConsumer>();

var host = builder.Build();
host.Run();