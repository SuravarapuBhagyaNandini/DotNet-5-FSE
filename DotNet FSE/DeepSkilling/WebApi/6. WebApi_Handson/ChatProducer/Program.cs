using Confluent.Kafka;

var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
const string topic = "chat-topic";

using var producer = new ProducerBuilder<Null, string>(config).Build();

Console.WriteLine("Chat Producer started. Type messages and press Enter to send. Type 'exit' to quit.");
while (true)
{
    var line = Console.ReadLine();
    if (line == null) break;
    if (line.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;
    try
    {
        var result = await producer.ProduceAsync(topic, new Message<Null, string> { Value = line });
        Console.WriteLine($"Delivered '{result.Value}' to {result.TopicPartitionOffset}");
    }
    catch (ProduceException<Null, string> e)
    {
        Console.WriteLine($"Delivery failed: {e.Error.Reason}");
    }
}

await producer.FlushAsync(TimeSpan.FromSeconds(5));
