using System.Text;
using OrderService.EventProcessing;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OrderService.AsyncDataServices;

//RabbitMQ client
public class MessageBusSubscriber : BackgroundService
{
    private readonly IModel? _channel;
    private readonly IConnection? _connection;
    private readonly IEventProcessor _eventProcessor;
    private readonly string? _queueName;

    public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
    {
        _eventProcessor = eventProcessor;

        var factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQHost"],
            Port = int.Parse(configuration["RabbitMQPort"]!)
        };

        //try to connect to RabbitMQ message bus
        try
        {
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _queueName = _channel.QueueDeclare().QueueName;

            _channel.ExchangeDeclare("trigger", ExchangeType.Fanout);
            _channel.QueueBind(_queueName, "trigger", "");

            _connection.ConnectionShutdown += OnConnectionShutdown;

            Console.WriteLine("---> Listening ");
        }
        catch (Exception e)
        {
            Console.WriteLine($"---> Could not connect to the Message Bus: {e.Message}");
        }
    }

    //receive events from message bus
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (_, ea) =>
        {
            Console.WriteLine("---> Event Received");
            var body = ea.Body; //get msg from bus
            var msg = Encoding.UTF8.GetString(body.ToArray()); //convert to string

            _eventProcessor.ProcessEvent(msg); //process it
        };

        _channel.BasicConsume(_queueName, true, consumer);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        Console.WriteLine("---> Message Bus disposed");

        if (_channel == null || !_channel.IsOpen) return;

        _channel.Close();
        _connection?.Close();
    }

    private static void OnConnectionShutdown(object? sender, ShutdownEventArgs e)
    {
        Console.WriteLine("---> RabbitMQ Connection Shutdown");
    }
}