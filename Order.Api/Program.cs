using Order.Api.Application;
using Order.Api.Infrastructure.Kafka;
using Shared.Kafka.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Kafka configuration
builder.Services.Configure<KafkaOptions>(
    builder.Configuration.GetSection("Kafka"));

builder.Services.AddSingleton<IOrderEventPublisher, OrderEventPublisher>();

// Add controllers and Swagger (already present by default)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();


app.Run();

