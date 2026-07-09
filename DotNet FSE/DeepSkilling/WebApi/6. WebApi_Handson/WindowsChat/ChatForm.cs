using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Confluent.Kafka;

public class ChatForm : Form
{
    private TextBox _txtMessages;
    private TextBox _txtInput;
    private Button _btnSend;

    public ChatForm()
    {
        Text = "Windows Chat (Kafka)";
        Width = 600; Height = 400;

        _txtMessages = new TextBox { Multiline = true, ReadOnly = true, Dock = DockStyle.Top, Height = 250 };
        _txtInput = new TextBox { Dock = DockStyle.Top, Height = 24 };
        _btnSend = new Button { Text = "Send", Dock = DockStyle.Top };

        _btnSend.Click += async (s, e) => await SendMessage(_txtInput.Text);

        Controls.Add(_btnSend);
        Controls.Add(_txtInput);
        Controls.Add(_txtMessages);

        Task.Run(() => StartConsumer());
    }

    private async Task SendMessage(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return;
        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
        using var producer = new ProducerBuilder<Null, string>(config).Build();
        await producer.ProduceAsync("chat-topic", new Message<Null, string> { Value = text });
        _txtInput.Clear();
    }

    private void StartConsumer()
    {
        var config = new ConsumerConfig { GroupId = Guid.NewGuid().ToString(), BootstrapServers = "localhost:9092", AutoOffsetReset = AutoOffsetReset.Earliest };
        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe("chat-topic");
        try
        {
            while (true)
            {
                var cr = consumer.Consume(CancellationToken.None);
                AppendMessage($"[{DateTime.Now:HH:mm:ss}] {cr.Message.Value}");
            }
        }
        catch { }
    }

    private void AppendMessage(string text)
    {
        if (_txtMessages.InvokeRequired)
        {
            _txtMessages.Invoke(new Action(() => _txtMessages.AppendText(text + Environment.NewLine)));
        }
        else
        {
            _txtMessages.AppendText(text + Environment.NewLine);
        }
    }
}
