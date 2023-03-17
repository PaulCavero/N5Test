using N5Test.Models.Kafka;

namespace N5Test.Services.KafkaProvider
{
    public interface IKafkaService
    {
        Task GetKafkaMessage();
        Task SendKafkaMessage(KafkaOperation kafkaOperation);
    }
}