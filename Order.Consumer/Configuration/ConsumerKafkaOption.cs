using Shared.Kafka.Configuration;

namespace Order.Consumer.Configuration
{
    public class ConsumerKafkaOptions : KafkaOptions
    {
        public string GroupId { get; set; }
        public string Topic { get; set; }
    }
}