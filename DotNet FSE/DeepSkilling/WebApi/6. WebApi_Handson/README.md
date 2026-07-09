# 6. WebApi_Handson — Kafka integration with C# (Chat apps)

## Outline
- Introduction to Kafka
- Kafka Architecture: Topics, Partitions, Brokers
- Kafka with .NET (Confluent.Kafka)
- Installation of Kafka on Windows (Zookeeper basics)
- Demo: Console chat apps + Windows Forms client

## Hands-on
1) Create a chat producer (console) that sends messages to `chat-topic`.
2) Create a chat consumer (console) that subscribes to `chat-topic` and prints messages.
3) (Optional) Windows Forms sample to send messages from a GUI and receive printed messages in separate consumer windows.

## Setup (Windows)
1. Download Kafka binary (e.g. from Apache Kafka website) and unpack.
2. Start Zookeeper (if using older Kafka versions):

```powershell
cd kafka_2.13-<version>\bin\windows
.\zookeeper-server-start.bat ..\..\config\zookeeper.properties
```

3. Start Kafka broker:

```powershell
.\kafka-server-start.bat ..\..\config\server.properties
```

4. Create topic (optional):

```powershell
.\kafka-topics.bat --create --bootstrap-server localhost:9092 --replication-factor 1 --partitions 1 --topic chat-topic
```

## .NET prerequisites
- .NET SDK installed (recommended: .NET 6/7/8)
- NuGet package: `Confluent.Kafka`

## Chat Producer (Console)
- Project: `ChatProducer`
- Usage: runs and prompts for message; enter text to send to Kafka topic `chat-topic`.

## Chat Consumer (Console)
- Project: `ChatConsumer`
- Usage: runs and prints incoming chat messages.

## Run sequence
1. Start Zookeeper and Kafka broker (see commands above).
2. Run one or more instances of `ChatConsumer`:

```powershell
cd DeepSkilling\WebApi\6. WebApi_Handson\ChatConsumer
dotnet restore
dotnet run
```

3. Run `ChatProducer` and send messages:

```powershell
cd DeepSkilling\WebApi\6. WebApi_Handson\ChatProducer
dotnet restore
dotnet run
```

Messages typed into the producer will be consumed by all running consumers.

## Reference links
- https://www.c-sharpcorner.com/article/apache-kafka-net-application/
- https://www.c-sharpcorner.com/article/step-by-step-installation-and-configuration-guide-of-apache-kafka-on-windows-ope/

## Demo output
Example consumer output (when server is running and consumer subscribed):

```
[2020-12-23 13:24:15] Received message: Hello from Producer
[2020-12-23 13:24:20] Received message: welcome
```

---

Files in this lab:
- `ChatProducer/` — console app producing messages to Kafka
- `ChatConsumer/` — console app consuming messages from Kafka
- `WindowsChat/` — optional WinForms sample skeleton
