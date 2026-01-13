using System.Text.Json;
using Confluent.Kafka;
using MediatR;
using Microsoft.Extensions.Options;
using Order.Consumer.Application.Commands;
using Order.Consumer.Configuration;
using Shared.Contracts.Events;
using Shared.Kafka.Topics;

namespace Order.Consumer.Infrastructure.Kafka
{
    public class OrderConsumer : BackgroundService
    {
        private readonly IMediator _mediator;
        private readonly ConsumerKafkaOptions _options;
        private readonly ILogger<OrderConsumer> _logger;

        public OrderConsumer(IMediator mediator, IOptions<ConsumerKafkaOptions> options, ILogger<OrderConsumer> logger)
        {
            _mediator = mediator;
            _options = options.Value;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _options.BootstrapServers,
                GroupId = _options.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest,

                // kafka labes all messages as processed, this prevents ot for cases of failure in handling
                EnableAutoCommit = false
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(KafkaTopics.CreateOrderTopic);

            _logger.LogInformation("Kafka consumer started for topic '{Topic}' on server '{BootstrapServers}'", KafkaTopics.CreateOrderTopic, _options.BootstrapServers);

            while (!stoppingToken.IsCancellationRequested)
            {
                var result = consumer.Consume(stoppingToken);

                var order = JsonSerializer.Deserialize<CreateOrderEvent>(result.Message.Value)!;

                await _mediator.Send(
                    new OrderCommand(order),
                    stoppingToken
                );
                consumer.Commit(result);

                _logger.LogInformation("Order customer id: {CustomerId} and topic: {Topic}", order.CustomerId, result.Topic);
            }
        }
    }
}