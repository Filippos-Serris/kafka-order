using System.Text.Json;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Options;
using Order.Consumer.Application.Commands;
using Order.Consumer.Configuration;
using Shared.Contracts.Events;

namespace Order.Consumer.Infrastructure.Kafka
{
    public class OrderConsumer : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly ConsumerKafkaOptions _options;

        public OrderConsumer(IConfiguration configuration, IMediator mediator, IOptions<ConsumerKafkaOptions> options)
        {
            _configuration = configuration;
            _mediator = mediator;
            _options = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _configuration["Kafka:BootstrapServers"],
                GroupId = _configuration["Kafka:GroupId"],
                AutoOffsetReset = AutoOffsetReset.Earliest,

                // kafka labes all messages as processed, this prevents ot for cases of failure in handling
                EnableAutoCommit = false
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(_configuration["Kafka:Topic"]);

            while (!stoppingToken.IsCancellationRequested)
            {
                var result = consumer.Consume(stoppingToken);

                var order = JsonSerializer.Deserialize<CreateOrderEvent>(result.Message.Value)!;

                await _mediator.Send(
                    new OrderCommand(order),
                    stoppingToken
                );
            }
        }
    }
}