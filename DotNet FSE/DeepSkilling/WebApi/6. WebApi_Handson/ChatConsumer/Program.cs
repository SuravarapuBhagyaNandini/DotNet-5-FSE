using Confluent.Kafka;

var config = new ConsumerConfig
{
    GroupId = "chat-consumer-group",
    BootstrapServers = "localhost:9092",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

const string topic = "chat-topic";

using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
consumer.Subscribe(topic);

Console.WriteLine("Chat Consumer started. Listening for messages...");
try
{
    while (true)
    {
        var cr = consumer.Consume(CancellationToken.None);
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Received message: {cr.Message.Value}");
    }
}
catch (OperationCanceledException)
{
    consumer.Close();
}
