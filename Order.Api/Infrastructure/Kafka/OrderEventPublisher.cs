using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using Order.Api.Application;
using Shared.Contracts.Events;
using Shared.Kafka.Configuration;
using Shared.Kafka.Topics;

namespace Order.Api.Infrastructure.Kafka
{
    public class OrderEventPublisher : IOrderEventPublisher, IDisposable
    {
        private readonly IProducer<string, string> _producer;
        public OrderEventPublisher(IOptions<KafkaOptions> kafkaOptions)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = kafkaOptions.Value.BootstrapServers
            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task PublishCreatedOrderEventAsync(CreateOrderEvent orderEvent, CancellationToken cancellationToken)
        {
            var message = new Message<string, string>
            {
                Key = orderEvent.OrderId.ToString(),
                Value = JsonSerializer.Serialize(orderEvent)
            };

            await _producer.ProduceAsync(KafkaTopics.CreateOrderTopic, message, cancellationToken);
        }

        public void Dispose()
        {
            _producer?.Dispose();
        }
    }
}