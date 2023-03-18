using Confluent.Kafka;
using N5Test.Broker.Loggings;
using N5Test.Models.Kafka;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace N5Test.Services.KafkaProvider
{
    public class KafkaService : IKafkaService
    {
        private readonly ILoggingBroker loggingBroker;

        public KafkaService(ILoggingBroker loggingBroker)
        {
            this.loggingBroker = loggingBroker;
        }
        public async Task SendKafkaMessage(KafkaOperation kafkaOperation)
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            using var producer = new ProducerBuilder<Null, string>(config).Build();

            try
            {
                if (kafkaOperation == null) { throw new ArgumentException("The parameter cant be null"); }
                string? state;

                var response = await producer.ProduceAsync("Operations-topic",
                new Message<Null, string>
                {
                    Value = JsonSerializer.Serialize(kafkaOperation)
                });
            }
            catch (ProduceException<Null, string> ex)
            {
                string errorMessage = $"An error occurred using SendKafkaMessage.";
                this.loggingBroker.LogError(ex);
                throw new ArgumentException(errorMessage, ex);
            }
        }

        public async Task GetKafkaMessage()
        {
            var config = new ConsumerConfig
            {
                GroupId = "operations-coonsumer-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using var consumer = new ConsumerBuilder<Null, string>(config).Build();
            consumer.Subscribe("Operations-topic");
            CancellationTokenSource token = new();
            try
            {
                    var response = consumer.Consume(token.Token);
                    if (response.Message != null)
                    {
                        var result = JsonSerializer.Deserialize<KafkaOperation>
                            (response.Message.Value);
                    }
            }
            catch (Exception ex)
            {
                string errorMessage = $"An error occurred using GetKafkaMessage.";
                this.loggingBroker.LogError(ex);
                throw new ArgumentException(errorMessage, ex);
            }

        }
    }
}
